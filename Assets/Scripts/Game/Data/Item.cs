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
				PlayerManager.Instance.Data.Inventory.SetEquipment(this);
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
				playerManager.Recovery(usableItem.RandomPower);
				playerManager.RemoveInventoryItem(this);
			break;
			default:
				Debug.LogWarning("未実装の使用可能アイテムです UsableItemType= " + usableItem.UsableItemType);
			break;
			}

		}
	}
}