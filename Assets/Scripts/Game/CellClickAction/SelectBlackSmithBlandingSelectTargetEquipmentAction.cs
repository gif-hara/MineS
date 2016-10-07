using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SelectBlackSmithBlandingSelectTargetEquipmentAction : CellClickActionBase
	{
		private Item item;

		public SelectBlackSmithBlandingSelectTargetEquipmentAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			Debug.AssertFormat(this.item != null, "アイテムがありません.");
			var equipmentData = this.item.InstanceData as EquipmentData;
			if(equipmentData.ExistBranding)
			{
				var baseEquipment = PlayerManager.Instance.Data.Inventory.SelectItem;
				InformationManager.OnConfirmSynthesisFinalCheck(baseEquipment, this.item, Calculator.GetSynthesisNeedMoney(baseEquipment, this.item));
			}
			else
			{
				InformationManager.OnNotEquipmentBrandingTarget();
				return;
			}

			BlackSmithManager.Instance.SetBrandingTargetEquipment(this.item);
			ConfirmManager.Instance.Add(ConfirmManager.Instance.decideSynthesis, BlackSmithManager.Instance.InvokeSynthesis, true);
			ConfirmManager.Instance.Add(ConfirmManager.Instance.cancel, null, true);
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