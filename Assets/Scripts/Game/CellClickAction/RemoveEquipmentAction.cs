using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class RemoveEquipmentAction : CellClickActionBase
	{
		private Inventory inventory;

		private Item item;

		public RemoveEquipmentAction(Inventory inventory, Item item, CellController cellController)
		{
			this.inventory = inventory;
			this.item = item;
			cellController.SetImage(this.item.InstanceData.Image);
		}

		public override void Invoke(CellData data)
		{
			if(this.inventory.IsFreeSpace)
			{
				inventory.RemoveEquipment(this.item);
				inventory.AddItem(this.item);
				var playerManager = PlayerManager.Instance;
				playerManager.NotifyCharacterDataObservers();
				playerManager.UpdateInventoryUI();
			}
			else
			{
				Debug.LogWarning("道具袋がいっぱいで外せませんでした.");
			}
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