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
			data.BindCellClickAction(new AcquireItemAction(this.item, this.cellController));
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