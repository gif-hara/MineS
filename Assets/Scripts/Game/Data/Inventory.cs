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

		public Equipment Equipment{ private set; get; }

		public Inventory()
		{
			this.Items = new List<Item>();
			for(int i = 0; i < GameDefine.InventoryItemMax; i++)
			{
				this.Items.Add(null);
			}
			this.Equipment = new Equipment();
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

		public void RemoveItem(Item item)
		{
			var index = this.Items.FindIndex(i => i == item);
			this.Items[index] = null;
		}

		public void ChangeItem(Item before, Item after)
		{
			var index = this.Items.FindIndex(i => i == before);
			this.Items[index] = after;
		}

		public bool IsFreeSpace
		{
			get
			{
				return this.Items.FindIndex(i => i == null) != -1;
			}
		}

		public Item ChangeEquipment(Item item)
		{
			return this.Equipment.Change(item);
		}

		public void RemoveEquipment(Item item)
		{
			this.Equipment.Remove(item);
		}
	}
}