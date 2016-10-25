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
	public class WareHouseData
	{
		[SerializeField]
		private Inventory inventory;

		public Inventory Inventory{ get { return this.inventory; } }

		private const string InventoryKeyName = "WareHouseInventory";

		public WareHouseData()
		{
			this.inventory = new Inventory(null, GameDefine.WareHouseInventoryMax);
			if(HK.Framework.SaveData.ContainsKey(InventoryKeyName))
			{
				this.inventory.Deserialize(InventoryKeyName);
			}
		}

		public bool Add(Item item)
		{
			var result = this.inventory.AddItem(item);
			if(result)
			{
				this.inventory.Serialize(InventoryKeyName);
			}
			return result;
		}

		public void Remove(Item item)
		{
			this.inventory.RemoveItem(item);
			this.inventory.Serialize(InventoryKeyName);
		}
	}
}