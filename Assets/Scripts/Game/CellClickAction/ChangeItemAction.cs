using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ChangeItemAction : CellClickActionBase
	{
		private Item item;

		public ChangeItemAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			if(this.item == null)
			{
				return;
			}

			var inventory = PlayerManager.Instance.Data.Inventory;
			inventory.InvokeExchangeItem(this.item);
			PlayerManager.Instance.CloseInventoryUI();
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			if(this.item != null)
			{
				cellController.SetImage(this.item.InstanceData.Image);
			}
		}

		public override void SetCellData(CellData data)
		{
			base.SetCellData(data);
			data.BindDeployDescription(new DeployDescriptionOnItem(this.item));
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