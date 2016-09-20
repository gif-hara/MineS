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

		public AcquireItemAction(Inventory inventory, Item item)
		{
			this.inventory = inventory;
			this.item = item;
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
				Debug.LogWarning("道具袋が一杯で取得できませんでした.");
			}
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