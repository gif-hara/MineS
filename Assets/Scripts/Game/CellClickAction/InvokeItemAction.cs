using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeItemAction : CellClickActionBase
	{
		private Item item;

		public InvokeItemAction(Item item, CellController cellController)
		{
			this.item = item;
			if(this.item != null)
			{
				cellController.SetImage(this.item.InstanceData.Image);
			}
		}

		public override void Invoke(CellData data)
		{
			if(this.item == null)
			{
				return;
			}
			item.Use();
			PlayerManager.Instance.UpdateInventoryUI();
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