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
			if(this.FindAbility(GameDefine.AbilityType.Reincarnation))
			{
				var blankCell = CellManager.Instance.RandomBlankCell;
				if(blankCell != null)
				{
					var enemy = EnemyManager.Instance.Create(blankCell);
					this.BindCombatEnemyAction(blankCell, enemy);
				}
			}

			var cellData = EnemyManager.Instance.InEnemyCells[this];
			cellData.BindCellClickAction(null);
			cellData.Controller.SetActiveStatusObject(false);
			cellData.Controller.SetImage(null);

			var adjacentCells = cellData.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(adjacentCells[i].IsLock)
				{
					adjacentCells[i].ReleaseLock();
				}
			}

			if(Calculator.CanDropItem(this.DropItemProbability, attacker))
			{
				var item = this.OverrideDropItems.Count > 0
					? new Item(this.OverrideDropItems[Random.Range(0, this.OverrideDropItems.Count)])
					: DungeonManager.Instance.CurrentData.CreateItem();
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
				var blankCell = CellManager.Instance.RandomBlankCell;
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
				var blankCell = CellManager.Instance.RandomBlankCell;
				if(blankCell != null)
				{
					var clone = new EnemyData();
					clone.Initialize(this.masterData);
					clone.HitPoint = this.HitPoint;
					this.BindCombatEnemyAction(blankCell, clone);
					EnemyManager.Instance.Add(blankCell, clone);
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
	}
}