using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateItemAction : CellClickActionBase
	{
		private Item item;

		public CreateItemAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			data.Controller.SetImage(this.item.InstanceData.Image);
			data.BindCellClickAction(new AcquireItemAction(PlayerManager.Instance.Data.Inventory, this.item));
		}

		public override void OnUseXray()
		{
			this.cellController.SetImage(this.item.InstanceData.Image);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Item;
			}
		}


	}
}