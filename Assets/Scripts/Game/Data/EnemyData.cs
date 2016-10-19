using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyData : CharacterData, IIdentification
	{
		public override void Dead(CharacterData attacker)
		{
			this.OnDead();

			if(this.FindAbility(GameDefine.AbilityType.Reincarnation))
			{
				var blankCell = CellManager.Instance.RandomBlankCell(true);
				if(blankCell != null)
				{
					var enemy = EnemyManager.Instance.Create(blankCell);
					this.BindCombatEnemyAction(blankCell, enemy);
				}
			}

			var cellData = EnemyManager.Instance.InEnemyCells[this];
			if(Calculator.CanDropItem(this.DropItemProbability, attacker))
			{
				var item = this.OverrideDropItems.Count > 0
					? new Item(this.OverrideDropItems[Random.Range(0, this.OverrideDropItems.Count)])
					: DungeonManager.Instance.CurrentDataAsDungeon.CreateItem();
				cellData.BindCellClickAction(new AcquireItemAction(item, cellData.Controller));
				cellData.BindDeployDescription(new DeployDescriptionOnItem(item));
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
			if(this.FindAbility(GameDefine.AbilityType.Summon) && !this.IsDead && Calculator.CanInvokeSummon)
			{
				var blankCell = CellManager.Instance.RandomBlankCell(true);
				if(blankCell != null)
				{
					var enemy = EnemyManager.Instance.Create(blankCell);
					this.BindCombatEnemyAction(blankCell, enemy);
				}
			}
		}

		public override void OnLateTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			base.OnLateTurnProgress(type, turnCount);
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
		}

		public override void ForceDead()
		{
			this.HitPoint = 0;
			this.OnDead();
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
		}

		public void OnDivision(CellData cellData)
		{
			var clone = new EnemyData();
			clone.Initialize(this.masterData, cellData.Controller);
			clone.HitPoint = this.HitPoint;
			clone.Armor = this.Armor;
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

		protected override void OnTakedDamage(CharacterData attacker, int value, bool onlyHitPoint)
		{
			base.OnTakedDamage(attacker, value, onlyHitPoint);
			if(this.FindAbility(GameDefine.AbilityType.Division) && !this.IsDead)
			{
				var blankCell = CellManager.Instance.RandomBlankCell(true);
				if(blankCell != null)
				{
					this.OnDivision(blankCell);
				}
			}
		}

		private void BindCombatEnemyAction(CellData cellData, EnemyData enemy)
		{
			cellData.BindCellClickAction(new CombatEnemyAction());
			cellData.BindDeployDescription(new DeployDescriptionOnCharacterData(enemy));
			cellData.Controller.SetCharacterData(enemy);
			cellData.Controller.SetImage(enemy.Image);
			var adjacentCells = cellData.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(!adjacentCells[i].IsIdentification)
				{
					adjacentCells[i].AddLock();
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

		private void OnDead()
		{
			this.cellController.DamageEffectCreator.ForceRemove();
			this.cellController.ForceRemoveImageShake();
			Object.Instantiate(EffectManager.Instance.prefabDeadEffect.Element, this.cellController.transform, false);

			var cellData = EnemyManager.Instance.InEnemyCells[this];
			cellData.BindCellClickAction(null);
			cellData.Controller.SetActiveStatusObject(false);
			cellData.Controller.SetImage(null);
			this.ReleaseLockAdjacentCells(cellData);
		}
	}
}