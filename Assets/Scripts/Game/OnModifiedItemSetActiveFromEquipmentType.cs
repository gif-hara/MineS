using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedItemSetActiveFromEquipmentType : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private GameObject strengthObject;

		[SerializeField]
		private GameObject armorObject;

		[SerializeField]
		private GameObject hitProbabilityObject;

		[SerializeField]
		private GameObject evasionObject;

		[SerializeField]
		private GameObject luckObject;

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
			this.strengthObject.SetActive(false);
			this.armorObject.SetActive(false);
			this.hitProbabilityObject.SetActive(false);
			this.evasionObject.SetActive(false);
			this.luckObject.SetActive(false);
			this.GetGameObject(item.InstanceData.ItemType).SetActive(true);
		}

		private GameObject GetGameObject(GameDefine.ItemType itemType)
		{
			switch(itemType)
			{
			case GameDefine.ItemType.Weapon:
				return this.strengthObject;
			case GameDefine.ItemType.Shield:
				return this.armorObject;
			case GameDefine.ItemType.Accessory:
				return this.luckObject;
			default:
				Debug.AssertFormat(false, "不正な値です. itemType = {0}", itemType);
				return null;
			}
		}

#endregion
	}
}