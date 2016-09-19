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
		public List<Item> Items{ private set; get; }

		public Inventory()
		{
			this.Items = new List<Item>();
			for(int i = 0; i < GameDefine.InventoryItemMax; i++)
			{
				this.Items.Add(null);
			}
		}

		public bool AddItem(Item item)
		{
			var emptyIndex = this.Items.FindIndex(i => i == null);
			if(emptyIndex < 0)
			{
				return false;
			}

			this.Items[emptyIndex] = item;

			return true;
		}
	}
}