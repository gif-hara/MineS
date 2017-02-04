using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public sealed class EnemyData : CharacterData, IIdentification
	{
		public override string Name
		{
			get
			{
				return string.Format(EnemyManager.Instance.NameFormat, this.name, this.level);
			}
		}
		public override void Dead(CharacterData attacker)
		{
		    base.Dead(attacker);
			this.OnDead(true);

			SEManager.Instance.PlaySE(SEManager.Instance.dead);

			if(this.FindAbility(GameDefine.AbilityType.Reincarnation))
			{
				var blankCell = CellManager.Instance.RandomBlankCell(true);
				if(blankCell != null)
				{
					var enemy = EnemyManager.Instance.Create(blankCell);
					EnemyManager.BindCombatEnemyAction(blankCell, enemy);
					Object.Instantiate(EffectManager.Instance.prefabSummon.Element, blankCell.Controller.transform, false);
				}
			}

			var cellData = EnemyManager.Instance.InEnemyCells[this];
			cellData.BindDeployDescription(null);
			if(Calculator.CanDropItem(this.DropItemProbability, attacker))
			{
				var item = this.OverrideDropItems.Count > 0
					? new Item(this.OverrideDropItems[Random.Range(0, this.OverrideDropItems.Count)])
					: DungeonManager.Instance.CurrentDataAsDungeon.CreateItem();
				cellData.BindCellClickAction(new AcquireItemAction(item));
				cellData.BindDeployDescription(new DeployDescriptionOnItem(item));
			}
		}

		public override void Defeat(IAttack target)
		{
			// 肩代の敵を倒した場合はプレイヤーが倒したことにする
			if(target.CharacterType == GameDefine.CharacterType.Enemy && target.FindAbnormalStatus(GameDefine.AbnormalStatusType.Assumption))
			{
                PlayerManager.Instance.Data.Defeat(target);
            }
		}

		public override string ColorCode
		{
			get
			{
				return GameDefine.BadColorCode;
			}
		}

		public void OnIdentification(CellData cellData)
		{
			this.Abilities.ForEach(a => a.OnIdentification(cellData));
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			base.OnTurnProgress(type, turnCount);

			if(this.FindAbnormalStatus(GameDefine.AbnormalStatusType.Confusion))
			{
				var targets = EnemyManager.Instance.VisibleEnemies;
				var target = targets[Random.Range(0, targets.Count)];
				var damage = target.TakeDamage(this, this.FinalStrength, false);
				InformationManager.ConfusionEnemyAttack(this, target, damage);
			}
		}

		public override void ForceLevelUp(int value)
		{
			if(this.masterData.NextLevelData == null)
			{
				InformationManager.OnHadNoEffect();
				return;
			}

			var currentName = this.Name;
			for(int i = 0; i < value; i++)
			{
				if(this.masterData.NextLevelData == null)
				{
					break;
				}
				this.Initialize(this.masterData.NextLevelData, this.cellController);
			}

			this.cellController.SetImage(this.Image);
			InformationManager.LevelUpEnemy(this, currentName, this.Name);
			Object.Instantiate(EffectManager.Instance.prefabSummon.Element, this.cellController.transform, false);
		}

		public override void ForceLevelDown(int value)
		{
			if(this.masterData.PreviousLevelData == null)
			{
				InformationManager.OnHadNoEffect();
				return;
			}

			var currentName = this.Name;
			for(int i = 0; i < value; i++)
			{
				if(this.masterData.PreviousLevelData == null)
				{
					break;
				}
				this.Initialize(this.masterData.PreviousLevelData, this.cellController);
			}

			this.cellController.SetImage(this.Image);
			InformationManager.LevelDownEnemy(this, currentName, this.Name);
			Object.Instantiate(EffectManager.Instance.prefabSummon.Element, this.cellController.transform, false);
		}

		public override void ForceDead()
		{
			this.hitPoint = 0;
			this.OnDead(true);
		}

		public override void ReturnTown()
		{
			this.hitPoint = 0;
			this.OnDead(false);
			InformationManager.ReturnTown(this);
		}

		public override void ChangeMasterData(CharacterMasterData masterData)
		{
			var currentName = this.Name;
			this.Initialize(masterData, this.cellController);
			this.cellController.SetImage(this.Image);
			InformationManager.ChangeCharacter(currentName, this);
			Object.Instantiate(EffectManager.Instance.prefabSummon.Element, this.cellController.transform, false);
		}

		public void OnVisible(CellData cellData)
		{
			cellData.BindCellClickAction(new CombatEnemyAction());
			cellData.BindDeployDescription(new DeployDescriptionOnCharacterData(this));
			cellData.Controller.SetCharacterData(this);
			cellData.Controller.SetImage(this.Image);
			var adjacentCells = cellData.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(!adjacentCells[i].IsIdentification)
				{
					adjacentCells[i].AddLock();
				}
			}

			if(CellManager.Instance.FindStoneStatue(GameDefine.StoneStatueType.Regeneration))
			{
				this.AddAbnormalStatus(AbnormalStatusFactory.Create(GameDefine.AbnormalStatusType.Regeneration, this, 5, 1));
			}

			if(CellManager.Instance.FindStoneStatue(GameDefine.StoneStatueType.Poison))
			{
				this.AddAbnormalStatus(AbnormalStatusFactory.Create(GameDefine.AbnormalStatusType.Poison, this, 5, 1));
			}
		}

		public void OnDivision(CellData cellData)
		{
			var clone = new EnemyData();
			clone.Initialize(this.masterData, cellData.Controller);
			clone.hitPoint = this.HitPoint;
			clone.baseArmor = this.Armor;
			EnemyManager.Instance.Add(cellData, clone);
			clone.OnVisible(cellData);
		}

		public override GameDefine.CharacterType CharacterType
		{
			get
			{
				return GameDefine.CharacterType.Enemy;
			}
		}

		public void Serialize(string key)
		{
			HK.Framework.SaveData.SetClass<EnemyData>(key, this);
			this.SerializeAbnormalStatuses(key);
		}

		public static EnemyData Deserialize(string key, CellController cellController)
		{
			var obj = HK.Framework.SaveData.GetClass<EnemyData>(key, null);
			obj.abilities = AbilityFactory.Create(obj.abilityTypes, obj);
			obj.cellController = cellController;
			DeserializeAbnormalStatuses(key, obj);
		    if (obj.IsAnyBuff)
		    {
		        obj.cellController.CreateBuffEffect();
		    }
		    if (obj.IsAnyDebuff)
		    {
		        obj.cellController.CreateDebuffEffect();
		    }
            return obj;
		}

		protected override void OnTakedDamage(CharacterData attacker, int value, bool onlyHitPoint)
		{
			base.OnTakedDamage(attacker, value, onlyHitPoint);
			if(this.FindAbility(GameDefine.AbilityType.Division) && !this.IsDead)
			{
				var blankCell = CellManager.Instance.RandomBlankCell(true);
				if(blankCell != null)
				{
					this.OnDivision(blankCell);
					Object.Instantiate(EffectManager.Instance.prefabSummon.Element, blankCell.Controller.transform, false);
				}
			}
		}

		private void ReleaseLockAdjacentCells(CellData cellData)
		{
			var adjacentCells = cellData.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(adjacentCells[i].IsLock)
				{
					adjacentCells[i].ReleaseLock();
				}
			}
		}

		private void OnDead(bool createDeadEffect)
		{
			this.cellController.DamageEffectCreator.ForceRemove();
			this.cellController.ForceRemoveImageShake();

			if(createDeadEffect)
			{
				Object.Instantiate(EffectManager.Instance.prefabDeadEffect.Element, this.cellController.transform, false);
			}

			var cellData = EnemyManager.Instance.InEnemyCells[this];
			cellData.BindCellClickAction(null);
			cellData.Controller.SetActiveStatusObject(false);
			cellData.Controller.SetImage(null);
			this.ReleaseLockAdjacentCells(cellData);
		}
	}
}