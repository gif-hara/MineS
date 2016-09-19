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
		private Item item;


		public AcquireItemAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			data.Controller.SetDebugText("");
			data.BindCellClickAction(null);
			PlayerManager.Instance.Data.Inventory.AddItem(this.item);
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