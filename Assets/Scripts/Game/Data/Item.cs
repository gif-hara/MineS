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
			Debug.Log(this.InstanceData.ItemName);
		}
	}
}