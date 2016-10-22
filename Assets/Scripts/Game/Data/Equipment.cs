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
	public class Equipment
	{
		public Item Weapon{ private set; get; }

		public Item Shield{ private set; get; }

		public Item Accessory{ private set; get; }

		public Item Change(Item item, CharacterData holder)
		{
			Item beforeItem = null;
			(item.InstanceData as EquipmentData).SetAbilitiesHolder(holder);
			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Weapon:
				beforeItem = this.Weapon;
				this.Weapon = item;
			break;
			case GameDefine.ItemType.Shield:
				beforeItem = this.Shield;
				this.Shield = item;
				holder.CheckArmorMax();
			break;
			case GameDefine.ItemType.Accessory:
				beforeItem = this.Accessory;
				this.Accessory = item;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}

			return beforeItem;
		}

		public void Remove(Item item)
		{
			if(item == null)
			{
				return;
			}
			(item.InstanceData as EquipmentData).SetAbilitiesHolder(null);

			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Weapon:
				this.Weapon = null;
			break;
			case GameDefine.ItemType.Shield:
				this.Shield = null;
			break;
			case GameDefine.ItemType.Accessory:
				this.Accessory = null;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}
		}

		public void RemoveAll()
		{
			this.Remove(this.Weapon);
			this.Remove(this.Shield);
			this.Remove(this.Accessory);
		}

		public Item Get(GameDefine.ItemType type)
		{
			return this.GetMatchEquipment(type);
		}

		/// <summary>
		/// 引数のアイテムが装備中であるか返す.
		/// </summary>
		/// <returns><c>true</c> if this instance is in equipment the specified item; otherwise, <c>false</c>.</returns>
		/// <param name="item">Item.</param>
		public bool IsInEquipment(Item item)
		{
			return this.Get(item.InstanceData.ItemType) == item;
		}

		private Item GetMatchEquipment(GameDefine.ItemType itemType)
		{
			switch(itemType)
			{
			case GameDefine.ItemType.Weapon:
				return this.Weapon;
			case GameDefine.ItemType.Shield:
				return this.Shield;
			case GameDefine.ItemType.Accessory:
				return this.Accessory;
			}

			Debug.AssertFormat(false, "不正な値です = {0}", itemType);
			return null;
		}

		private int GetPower(Item item)
		{
			return item == null ? 0 : (item.InstanceData as EquipmentData).Power;
		}

		public List<Item> ToList
		{
			get
			{
				var result = new List<Item>();
				result.Add(this.Weapon);
				result.Add(this.Shield);
				result.Add(this.Accessory);

				return result;
			}
		}

		public int TotalStrength
		{
			get
			{
				return this.GetPower(this.Weapon);
			}
		}

		public int TotalArmor
		{
			get
			{
				return this.GetPower(this.Shield);
			}
		}

		public int TotalLuck
		{
			get
			{
				return this.GetPower(this.Accessory);
			}
		}
	}
}