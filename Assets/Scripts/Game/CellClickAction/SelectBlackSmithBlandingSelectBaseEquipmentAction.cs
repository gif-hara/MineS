using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SelectBlackSmithBlandingSelectBaseEquipmentAction : CellClickActionBase
	{
		private Item item;

		public SelectBlackSmithBlandingSelectBaseEquipmentAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			Debug.AssertFormat(this.item != null, "アイテムがありません.");
			var playerManager = PlayerManager.Instance;

			var equipmentData = this.item.InstanceData as EquipmentData;
			if(equipmentData.CanBranding)
			{
				InformationManager.OnConfirmBrandingSelectTargetEquipment();
			}
			else
			{
				InformationManager.OnNotEquipmentBranding();
				return;
			}

			playerManager.Data.Inventory.SetSelectItem(this.item);
			playerManager.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_BrandingSelectTargetEquipment);
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			if(this.item != null)
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