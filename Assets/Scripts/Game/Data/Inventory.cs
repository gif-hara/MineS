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
	public class Inventory
	{
		private List<Item> items;

		public Inventory()
		{
			this.items = new List<Item>();
			for(int i = 0; i < GameDefine.InventoryItemMax; i++)
			{
				this.items.Add(null);
			}
		}

		public bool AddItem(Item item)
		{
			var emptyIndex = this.items.FindIndex(i => i == null);
			if(emptyIndex < 0)
			{
				return false;
			}

			this.items[emptyIndex] = item;

			return true;
		}
	}
}