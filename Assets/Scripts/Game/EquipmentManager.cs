using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EquipmentManager : SingletonMonoBehaviour<EquipmentManager>
	{
		[System.Serializable]
		public class StreetName
		{
			[SerializeField]
			private StringAsset.Finder level1 = null, level2 = null, level3 = null;

			public string Get(int level)
			{
				if(level <= 0)
				{
					return "";
				}

				return this.ToArray[level - 1].Get;
			}

			private StringAsset.Finder[] ToArray
			{
				get
				{
					return new StringAsset.Finder[]{ this.level1, this.level2, this.level3 };
				}
			}
		}

		[SerializeField]
		private StreetName weaponStreetName;

		[SerializeField]
		private StreetName shieldStreetName;

		public string GetStreetName(EquipmentData equipmentData)
		{
			return equipmentData.ItemType == GameDefine.ItemType.Weapon
				? this.weaponStreetName.Get(equipmentData.Level)
				: this.shieldStreetName.Get(equipmentData.Level);
		}
	}
}