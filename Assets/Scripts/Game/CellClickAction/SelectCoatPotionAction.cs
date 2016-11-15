using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SelectCoatPotionAction : CellClickActionBase
	{
		private Item item;

		public SelectCoatPotionAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			Debug.AssertFormat(this.item != null, "アイテムがありません.");
			PlayerManager.Instance.RemoveInventoryItem(this.item);
			var selectItem = PlayerManager.Instance.Data.Inventory.SelectItem;
			var throwingInstanceData = selectItem.InstanceData as ThrowingInstanceData;
			throwingInstanceData.Coating(this.item.InstanceData as UsableItemInstanceData);
			if(throwingInstanceData.IsEmpty)
			{
				PlayerManager.Instance.RemoveInventoryItem(selectItem);
			}
			PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.Use, PlayerManager.Instance.Data.Inventory);
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			if(this.item != null && this.item.IsValid)
			{
				this.cellController.SetImage(this.item.InstanceData.Image);
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