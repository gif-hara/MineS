using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedInventoryApplyEquipmentToCell : MonoBehaviour, IReceiveModifiedInventory
	{
		[SerializeField]
		private CellController weaponCellController;

		[SerializeField]
		private CellController shieldCellController;

		[SerializeField]
		private CellController helmetCellController;

		[SerializeField]
		private CellController bodyCellController;

		[SerializeField]
		private CellController gloveCellController;

		[SerializeField]
		private CellController legCellController;

		[SerializeField]
		private CellController accessoryCellController;

#region IReceiveModifiedInventory implementation

		public void OnModifiedInventory(Inventory data)
		{
			var equipment = PlayerManager.Instance.Data.Inventory.Equipment;
			this.Apply(this.weaponCellController, equipment.Weapon);
			this.Apply(this.shieldCellController, equipment.Shield);
			this.Apply(this.helmetCellController, equipment.Helmet);
			this.Apply(this.bodyCellController, equipment.Body);
			this.Apply(this.gloveCellController, equipment.Glove);
			this.Apply(this.legCellController, equipment.Leg);
			this.Apply(this.accessoryCellController, equipment.Accessory);
		}

		private void Apply(CellController cellController, Item item)
		{
			var cellData = new CellData();
			cellData.SetController(cellController);
			cellController.SetCellData(cellData);
			if(item == null)
			{
				return;
			}
			cellData.BindCellClickAction(new RemoveEquipmentAction(PlayerManager.Instance.Data.Inventory, item, cellController));
		}

#endregion
	}
}