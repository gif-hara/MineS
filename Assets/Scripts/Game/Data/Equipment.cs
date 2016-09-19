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
		public EquipmentData Accessory{ private set; get; }

		public EquipmentData Body{ private set; get; }

		public EquipmentData Glove{ private set; get; }

		public EquipmentData Helmet{ private set; get; }

		public EquipmentData Leg{ private set; get; }

		public EquipmentData Shield{ private set; get; }

		public EquipmentData Weapon{ private set; get; }

		public void Set(Item item)
		{
			Debug.Log("SetEquipment! " + item.InstanceData.ItemName);
			switch(item.InstanceData.ItemType)
			{
			case GameDefine.ItemType.Accessory:
				this.Accessory = item.InstanceData as EquipmentData;
			break;
			case GameDefine.ItemType.Body:
				this.Body = item.InstanceData as EquipmentData;
			break;
			case GameDefine.ItemType.Glove:
				this.Glove = item.InstanceData as EquipmentData;
			break;
			case GameDefine.ItemType.Helmet:
				this.Helmet = item.InstanceData as EquipmentData;
			break;
			case GameDefine.ItemType.Leg:
				this.Leg = item.InstanceData as EquipmentData;
			break;
			case GameDefine.ItemType.Shield:
				this.Shield = item.InstanceData as EquipmentData;
			break;
			case GameDefine.ItemType.Weapon:
				this.Weapon = item.InstanceData as EquipmentData;
			break;
			default:
				Debug.AssertFormat(false, "不正な値です = {0}", item.InstanceData.ItemType);
			break;
			}
		}

		private EquipmentData GetMatchEquipment(GameDefine.ItemType itemType)
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
	}
}