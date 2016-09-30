using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ExchangeItemController
	{
		private Item exchangeItem;

		private CellData fieldCell;

		private Inventory inventory;

		public ExchangeItemController(Inventory inventory)
		{
			this.inventory = inventory;
		}

		public void Initialize(Item item, CellData fieldCell)
		{
			this.exchangeItem = item;
			this.fieldCell = fieldCell;
		}

		public void Invoke(Item inventoryItem)
		{
			this.inventory.ChangeItem(inventoryItem, this.exchangeItem);
			this.fieldCell.BindCellClickAction(new AcquireItemAction(this.inventory, inventoryItem, this.fieldCell.Controller));
			this.fieldCell.BindDeployDescription(new DeployDescriptionOnItem(inventoryItem));
			this.exchangeItem = null;
		}

		public bool CanExchange
		{
			get
			{
				return this.exchangeItem != null;
			}
		}
	}
}