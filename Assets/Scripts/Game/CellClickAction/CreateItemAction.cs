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

		public CreateItemAction()
		{
			
		}

		public CreateItemAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			data.BindCellClickAction(new AcquireItemAction(this.item));
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

		public override void Serialize(int y, int x)
		{
			this.item.Serialize(this.ItemSerializeKeyName(y, x));
		}

		public override void Deserialize(int y, int x)
		{
			this.item = Item.Deserialize(this.ItemSerializeKeyName(y, x));
		}

		private string ItemSerializeKeyName(int y, int x)
		{
			return string.Format("CreateItemActionItem_{0}_{1}", y, x);
		}
	}
}