using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class EquipmentMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private int power;

		[SerializeField]
		private GameDefine.ItemType itemType;

		[SerializeField]
		private List<int> spells;

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}
	}
}