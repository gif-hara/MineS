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
	public class StoneStatueSpringWater : StoneStatue
	{
		public StoneStatueSpringWater(CellData cellData)
			: base(GameDefine.StoneStatueType.SpringWater, cellData)
		{
		}

		public override void OnLateTurnProgress()
		{
			if(!Calculator.CanCreateStoneStatueSpringWater)
			{
				return;
			}

			var adjacentCells = CellManager.Instance.GetAdjacentCellDataAll(this.cellData.Position)
				.Where(c => c.CurrentEventType == GameDefine.EventType.None)
				.ToList();
			if(adjacentCells.Count <= 0)
			{
				return;
			}
			var usableItemMasterData = DungeonManager.Instance.CurrentDataAsDungeon.ItemTable.Elements
				.Where(e => e.MasterData.ItemType == GameDefine.ItemType.UsableItem)
				.ToList();
			var item = new Item(usableItemMasterData[Random.Range(0, usableItemMasterData.Count)].MasterData);
			var cell = adjacentCells[Random.Range(0, adjacentCells.Count)];
			cell.BindCellClickAction(new AcquireItemAction(item));
			cell.BindDeployDescription(new DeployDescriptionOnItem(item));
			Object.Instantiate(EffectManager.Instance.prefabCreateUsableItemSpringWater.Element, cell.Controller.transform, false);
		}
	}
}