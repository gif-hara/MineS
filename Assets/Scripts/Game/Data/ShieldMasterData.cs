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
	public class ShieldMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private int power;

		[SerializeField]
		private List<int> spells;

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.Shield;
			}
		}
	}
}