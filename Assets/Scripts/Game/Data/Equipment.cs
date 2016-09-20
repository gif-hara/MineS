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
		public Item Accessory{ private set; get; }

		public Item Body{ private set; get; }

		public Item Glove{ private set; get; }

		public Item Helmet{ private set; get; }

		public Item Leg{ private set; get; }

		public Item Shield{ private set; get; }

		public Item Weapon{ private set; get; }

		public Item Change(Item item)
		{
			Item beforeItem = null;
			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Accessory:
				beforeItem = this.Accessory;
				this.Accessory = item;
			break;
			case GameDefine.ItemType.Body:
				beforeItem = this.Body;
				this.Body = item;
			break;
			case GameDefine.ItemType.Glove:
				beforeItem = this.Glove;
				this.Glove = item;
			break;
			case GameDefine.ItemType.Helmet:
				beforeItem = this.Helmet;
				this.Helmet = item;
			break;
			case GameDefine.ItemType.Leg:
				beforeItem = this.Leg;
				this.Leg = item;
			break;
			case GameDefine.ItemType.Shield:
				beforeItem = this.Shield;
				this.Shield = item;
			break;
			case GameDefine.ItemType.Weapon:
				beforeItem = this.Weapon;
				this.Weapon = item;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}

			return beforeItem;
		}

		public void Remove(Item item)
		{
			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Accessory:
				this.Accessory = null;
			break;
			case GameDefine.ItemType.Body:
				this.Body = null;
			break;
			case GameDefine.ItemType.Glove:
				this.Glove = null;
			break;
			case GameDefine.ItemType.Helmet:
				this.Helmet = null;
			break;
			case GameDefine.ItemType.Leg:
				this.Leg = null;
			break;
			case GameDefine.ItemType.Shield:
				this.Shield = null;
			break;
			case GameDefine.ItemType.Weapon:
				this.Weapon = null;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}
		}

		private Item GetMatchEquipment(GameDefine.ItemType itemType)
		{
			switch(itemType)
			{
			case GameDefine.ItemType.Accessory:
				return this.Accessory;
			case GameDefine.ItemType.Body:
				return this.Body;
			case GameDefine.ItemType.Glove:
				return this.Glove;
			case GameDefine.ItemType.Helmet:
				return this.Helmet;
			case GameDefine.ItemType.Leg:
				return this.Leg;
			case GameDefine.ItemType.Shield:
				return this.Shield;
			case GameDefine.ItemType.Weapon:
				return this.Weapon;
			}

			Debug.AssertFormat(false, "不正な値です = {0}", itemType);
			return null;
		}

		private int GetPower(Item item)
		{
			return item == null ? 0 : (item.InstanceData as EquipmentData).Power;
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
				return this.GetPower(this.Helmet) + this.GetPower(this.Body) + this.GetPower(this.Glove) + this.GetPower(this.Shield);
			}
		}

		public int TotalEvasion
		{
			get
			{
				return this.GetPower(this.Leg);
			}
		}
	}
}