using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SelectBlackSmithRemoveAbilitySelectBaseEquipmentAction : CellClickActionBase
	{
		private Item item;

		public SelectBlackSmithRemoveAbilitySelectBaseEquipmentAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			if(this.item == null)
			{
				return;
			}
			BlackSmithManager.Instance.SetRemoveAbilityBaseEquipment(this.item);
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