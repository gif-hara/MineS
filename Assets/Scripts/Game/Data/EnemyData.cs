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
				return "#FF4E4E";
			}
		}

		public void OnIdentification(CellData cellData)
		{
			this.Abilities.ForEach(a => a.OnIdentification(cellData));
		}

		protected override void OnTakedDamage(CharacterData attacker, int value, bool onlyHitPoint)
		{
			base.OnTakedDamage(attacker, value, onlyHitPoint);
			if(this.FindAbility(GameDefine.AbilityType.Division) && !this.IsDead)
			{
				var blankCells = CellManager.Instance.ToListCellData;
				blankCells = blankCells.Where(c => c.IsIdentification && c.CurrentEventType == GameDefine.EventType.None).ToList();
				if(blankCells.Count > 0)
				{
					var clone = new EnemyData();
					clone.Initialize(this.masterData);
					clone.HitPoint = this.HitPoint;
					var blankCell = blankCells[Random.Range(0, blankCells.Count)];
					blankCell.BindCellClickAction(new CombatEnemyAction());
					blankCell.BindDeployDescription(new DeployDescriptionOnCharacterData(clone));
					blankCell.Controller.SetCharacterData(clone);
					blankCell.Controller.SetImage(clone.Image);
					EnemyManager.Instance.Add(blankCell, clone);

					var adjacentCells = blankCell.AdjacentCellAll;
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
	}
}