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
	public class HelmetMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private int power;

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.Helmet;
			}
		}
	}
}