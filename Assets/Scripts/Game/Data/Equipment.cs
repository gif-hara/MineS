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
		[SerializeField]
		private Item weapon;

		[SerializeField]
		private Item shield;

		[SerializeField]
		private Item accessory;

		public Item Weapon{ get { return this.weapon; } }

		public Item Shield{ get { return this.shield; } }

		public Item Accessory{ get { return this.accessory; } }

		public void Serialize(string key)
		{
			HK.Framework.SaveData.SetInt(key, 1);
			if(this.weapon != null)
			{
				this.weapon.Serialize(this.WeaponKey(key));
			}
			else
			{
				HK.Framework.SaveData.Remove(this.WeaponKey(key));
			}
			if(this.shield != null)
			{
				this.shield.Serialize(this.ShieldKey(key));
			}
			else
			{
				HK.Framework.SaveData.Remove(this.ShieldKey(key));
			}
			if(this.accessory != null)
			{
				this.accessory.Serialize(this.AccessoryKey(key));
			}
			else
			{
				HK.Framework.SaveData.Remove(this.AccessoryKey(key));
			}
		}

		public void Deserialize(string key, CharacterData holder)
		{
			if(!HK.Framework.SaveData.ContainsKey(key))
			{
				return;
			}

			if(HK.Framework.SaveData.ContainsKey(this.WeaponKey(key)))
			{
				this.weapon = Item.Deserialize(this.WeaponKey(key));
				(this.weapon.InstanceData as EquipmentInstanceData).SetAbilitiesHolder(holder);
			}
			if(HK.Framework.SaveData.ContainsKey(this.ShieldKey(key)))
			{
				this.shield = Item.Deserialize(this.ShieldKey(key));
				(this.shield.InstanceData as EquipmentInstanceData).SetAbilitiesHolder(holder);
			}
			if(HK.Framework.SaveData.ContainsKey(this.AccessoryKey(key)))
			{
				this.accessory = Item.Deserialize(this.AccessoryKey(key));
				(this.accessory.InstanceData as EquipmentInstanceData).SetAbilitiesHolder(holder);
			}
		}

		private string WeaponKey(string key)
		{
			return string.Format("{0}_Weapon", key);
		}

		private string ShieldKey(string key)
		{
			return string.Format("{0}_Shield", key);
		}

		private string AccessoryKey(string key)
		{
			return string.Format("{0}_Accessory", key);
		}

		public Item Change(Item item, CharacterData holder)
		{
			SEManager.Instance.PlaySE(SEManager.Instance.equipOn);
			Item beforeItem = null;
			(item.InstanceData as EquipmentInstanceData).SetAbilitiesHolder(holder);
			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Weapon:
				beforeItem = this.weapon;
				this.weapon = item;
			break;
			case GameDefine.ItemType.Shield:
				beforeItem = this.shield;
				this.shield = item;
				holder.CheckArmorMax();
			break;
			case GameDefine.ItemType.Accessory:
				beforeItem = this.accessory;
				this.accessory = item;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}

			return beforeItem;
		}

		public void Remove(Item item, CharacterData holder)
		{
			if(item == null)
			{
				return;
			}
			SEManager.Instance.PlaySE(SEManager.Instance.equipOff);
			(item.InstanceData as EquipmentInstanceData).SetAbilitiesHolder(null);

			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Weapon:
				this.weapon = null;
			break;
			case GameDefine.ItemType.Shield:
				this.shield = null;
				holder.CheckArmorMax();
			break;
			case GameDefine.ItemType.Accessory:
				this.accessory = null;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}
		}

		public void RemoveAll(CharacterData holder)
		{
			this.Remove(this.weapon, holder);
			this.Remove(this.shield, holder);
			this.Remove(this.accessory, holder);
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
				return this.weapon;
			case GameDefine.ItemType.Shield:
				return this.shield;
			case GameDefine.ItemType.Accessory:
				return this.accessory;
			}

			Debug.AssertFormat(false, "不正な値です = {0}", itemType);
			return null;
		}

		private int GetPower(Item item)
		{
			return item == null ? 0 : (item.InstanceData as EquipmentInstanceData).Power;
		}

		public List<Item> ToList
		{
			get
			{
				var result = new List<Item>();
				result.Add(this.weapon);
				result.Add(this.shield);
				result.Add(this.accessory);

				return result;
			}
		}

		public int TotalStrength
		{
			get
			{
				return this.GetPower(this.weapon);
			}
		}

		public int TotalArmor
		{
			get
			{
				return this.GetPower(this.shield);
			}
		}

		public int TotalLuck
		{
			get
			{
				return this.GetPower(this.accessory);
			}
		}
	}
}