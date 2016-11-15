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
		[SerializeField]
		private List<Item> items;

		[SerializeField]
		private Equipment equipment;

		public List<Item> Items
		{
			get
			{
				this.ClearInvalidItem();
				return this.items;
			}
		}

		public Equipment Equipment{ get { return this.equipment; } }

		public ExchangeItemController ExchangeItemController{ private set; get; }

		private CharacterData holder;

		public Item SelectItem{ private set; get; }

		public GameDefine.InventoryModeType OpenType{ private set; get; }

		public Inventory(CharacterData holder, int itemMax)
		{
			this.holder = holder;
			this.items = new List<Item>();
			for(int i = 0; i < itemMax; i++)
			{
				this.items.Add(null);
			}
			this.equipment = new Equipment();
			this.ExchangeItemController = new ExchangeItemController(this);
		}

		public bool AddItem(Item item)
		{
			// 投擲物の加算処理.
			if(item.InstanceData.ItemType == GameDefine.ItemType.Throwing)
			{
				var existItem = this.items.Find(i => this.CanIntegrationThrowing(i, item));
				if(existItem != null)
				{
					(existItem.InstanceData as ThrowingInstanceData).Add((item.InstanceData as ThrowingInstanceData).RemainingNumber);
					return true;
				}
			}

			var emptyIndex = this.items.FindIndex(i => i == null || i.InstanceData == null);
			if(emptyIndex < 0)
			{
				return false;
			}

			this.items[emptyIndex] = item;

			return true;
		}

		public void AddItemNoLimit(Item item)
		{
			if(!this.AddItem(item))
			{
				this.items.Add(item);
			}
		}

		public void RemoveItem(Item item)
		{
			var index = this.items.FindIndex(i => i == item);
			if(index == -1)
			{
				return;
			}
			this.items[index] = null;
		}

		public void RemoveAll()
		{
			for(int i = 0; i < this.items.Count; i++)
			{
				this.items[i] = null;
			}
			this.equipment.RemoveAll(this.holder);
		}

		public void ChangeItem(Item before, Item after)
		{
			var index = this.items.FindIndex(i => i == before);
			this.items[index] = after;
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
				return this.items.FindIndex(i => i == null) != -1;
			}
		}

		public Item ChangeEquipment(Item item)
		{
			return this.equipment.Change(item, this.holder);
		}

		public void RemoveEquipment(Item item)
		{
			this.equipment.Remove(item, this.holder);
		}

		public void RemoveItemOrEquipment(Item item)
		{
			if(GameDefine.IsEquipment(item.InstanceData.ItemType) && this.equipment.IsInEquipment(item))
			{
				this.equipment.Remove(item, this.holder);
			}
			else
			{
				this.RemoveItem(item);
			}
		}

		public void Sort()
		{
			var result = new List<Item>();
			result.AddRange(this.items.Where(i => i != null && i.InstanceData.ItemType == GameDefine.ItemType.Weapon).OrderBy(i => i.InstanceData.Id));
			result.AddRange(this.items.Where(i => i != null && i.InstanceData.ItemType == GameDefine.ItemType.Shield).OrderBy(i => i.InstanceData.Id));
			result.AddRange(this.items.Where(i => i != null && i.InstanceData.ItemType == GameDefine.ItemType.Accessory).OrderBy(i => i.InstanceData.Id));
			result.AddRange(this.items.Where(i => i != null && i.InstanceData.ItemType == GameDefine.ItemType.Throwing).OrderBy(i => i.InstanceData.Id));
			result.AddRange(this.items.Where(i => i != null && i.InstanceData.ItemType == GameDefine.ItemType.UsableItem).OrderBy(i => i.InstanceData.Id));
			for(int i = 0, imax = this.items.Count - result.Count; i < imax; i++)
			{
				result.Add(null);
			}

			this.items = result;
		}

		public void Serialize(string key)
		{
			HK.Framework.SaveData.SetInt(key, 1);
			for(int i = 0; i < this.items.Count; i++)
			{
				var item = this.items[i];
				HK.Framework.SaveData.SetInt(this.ItemNullKey(key, i), item == null ? 0 : 1);
				if(item != null)
				{
					this.items[i].Serialize(this.ItemKey(key, i));
				}
				else
				{
					HK.Framework.SaveData.Remove(this.ItemKey(key, i));
				}
			}

			this.equipment.Serialize(key);
		}

		public void Deserialize(string key)
		{
			if(!HK.Framework.SaveData.ContainsKey(key))
			{
				Debug.AssertFormat(false, "{0}に対応するセーブデータがありません.", key);
				return;
			}
			for(int i = 0; i < this.items.Count; i++)
			{
				if(HK.Framework.SaveData.GetInt(this.ItemNullKey(key, i)) == 0)
				{
					continue;
				}
				this.items[i] = Item.Deserialize(this.ItemKey(key, i));
			}

			this.equipment.Deserialize(key, this.holder);
		}

		private string ItemKey(string key, int index)
		{
			return string.Format("{0}_Inventory_Item{1}", key, index);
		}

		private string ItemNullKey(string key, int index)
		{
			return string.Format("{0}_Inventory_ItemIsNull{1}", key, index);
		}

		public List<Item> AllItem
		{
			get
			{
				var result = new List<Item>();
				result.AddRange(this.items.Where(i => i != null));
				result.AddRange(this.equipment.ToList.Where(i => i != null));

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

		public bool IsPossessionAny
		{
			get
			{
				var list = this.AllItem;
				return list.Count <= 0;
			}
		}

		private void ClearInvalidItem()
		{
			for(int i = 0; i < this.items.Count; i++)
			{
				if(this.items[i] != null && !this.items[i].IsValid)
				{
					this.items[i] = null;
				}
			}
		}

		private bool CanIntegrationThrowing(Item a, Item b)
		{
			if(a == null || b == null)
			{
				return false;
			}

			var a_throwingItem = a.InstanceData as ThrowingInstanceData;
			var b_throwingItem = b.InstanceData as ThrowingInstanceData;
			if(a_throwingItem == null || b_throwingItem == null)
			{
				return false;
			}

			return a_throwingItem.Id == b_throwingItem.Id && a_throwingItem.CoatingId == b_throwingItem.CoatingId;

		}
	}
}