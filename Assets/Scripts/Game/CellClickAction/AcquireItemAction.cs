using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AcquireItemAction : CellClickActionBase
	{
		private Inventory inventory;

		private Item item;

		public AcquireItemAction(Inventory inventory, Item item, CellController cellController)
		{
			this.inventory = inventory;
			this.item = item;
			cellController.SetImage(this.item.InstanceData.Image);
		}

		public override void Invoke(CellData data)
		{
			if(this.inventory.IsFreeSpace)
			{
				data.Controller.SetImage(null);
				data.BindCellClickAction(null);
				this.inventory.AddItem(this.item);
			}
			else
			{
				this.inventory.SetExchangeItem(this.item, data);
				PlayerManager.Instance.OpenInventoryUI();
				DescriptionManager.Instance.DeployEmergency("ExchangeItem");
			}
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Item;
			}
		}

		public override Sprite Image
		{
			get
			{
				return this.item.InstanceData.Image;
			}
		}
	}
}