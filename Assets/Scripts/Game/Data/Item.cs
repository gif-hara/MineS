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
	public class Item
	{
		private ItemMasterDataBase masterData;

		public Item(ItemMasterDataBase masterData)
		{
			this.masterData = masterData;
		}
	}
}