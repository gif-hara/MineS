using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

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
	}
}