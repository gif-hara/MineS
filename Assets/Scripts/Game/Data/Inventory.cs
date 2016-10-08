using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

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

		public ExchangeItemController ExchangeItemController{ private set; get; }

		private CharacterData holder;

		public Item SelectItem{ private set; get; }

		public GameDefine.InventoryModeType OpenType{ private set; get; }

		public Inventory(CharacterData holder, int itemMax)
		{
			this.holder = holder;
			this.Items = new List<Item>();
			for(int i = 0; i < itemMax; i++)
			{
				this.Items.Add(null);
			}
			this.Equipment = new Equipment();
			this.ExchangeItemController = new ExchangeItemController(this);
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

		public void SetExchangeItem(Item exchangeItem, CellData fieldCell)
		{
			this.ExchangeItemController.Initialize(exchangeItem, fieldCell);
		}

		public void InvokeExchangeItem(Item inventoryItem)
		{
			this.ExchangeItemController.Invoke(inventoryItem);
		}

		public void SetSelectItem(Item item)
		{
			this.SelectItem = item;
		}

		public void UseSelectItem()
		{
			this.SelectItem.Use(this.holder);
		}

		public void SetMode(GameDefine.InventoryModeType type)
		{
			this.OpenType = type;
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
			return this.Equipment.Change(item, this.holder);
		}

		public Item ChangeEquipmentFromSelectItem()
		{
			return this.Equipment.Change(this.SelectItem, this.holder);
		}

		public void RemoveEquipment(Item item)
		{
			this.Equipment.Remove(item);
		}

		public void RemoveItemOrEquipment(Item item)
		{
			if(this.Equipment.IsInEquipment(item))
			{
				this.Equipment.Remove(item);
			}
			else
			{
				this.RemoveItem(item);
			}
		}

		public List<Item> AllItem
		{
			get
			{
				var result = new List<Item>();
				result.AddRange(this.Items.Where(i => i != null));
				result.AddRange(this.Equipment.ToList.Where(i => i != null));

				return result;
			}
		}

		public bool IsPossessionEquipment
		{
			get
			{
				var list = this.AllItem;
				return list.Exists(i => GameDefine.IsEquipment(i.InstanceData.ItemType));
			}
		}
	}
}