using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class Item
	{
		public ItemDataBase InstanceData{ private set; get; }

		public Item(ItemDataBase masterData)
		{
			this.InstanceData = masterData.Clone;
		}

		public void Use()
		{
			if(GameDefine.IsEquipment(InstanceData.ItemType))
			{
				var changedEquipment = PlayerManager.Instance.Data.Inventory.ChangeEquipment(this);
				PlayerManager.Instance.ChangeItem(this, changedEquipment);
			}
			else if(this.InstanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.UseUsableItem();
			}
			else
			{
				Debug.LogWarning("未実装のアイテムです ItemType = " + this.InstanceData.ItemType);
			}
		}

		public void UseUsableItem()
		{
			var playerManager = PlayerManager.Instance;
			var usableItem = this.InstanceData as UsableItemData;
			switch(usableItem.UsableItemType)
			{
			case GameDefine.UsableItemType.RecoveryHitPoint:
				playerManager.RecoveryHitPoint(usableItem.RandomPower, true);
				playerManager.RemoveInventoryItem(this);
			break;
			default:
				Debug.LogWarning("未実装の使用可能アイテムです UsableItemType= " + usableItem.UsableItemType);
			break;
			}

		}
	}
}