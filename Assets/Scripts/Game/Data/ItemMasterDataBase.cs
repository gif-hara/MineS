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
	public abstract class ItemMasterDataBase
	{
		[SerializeField]
		private StringAsset.Finder name;

		[SerializeField]
		private GameDefine.RareType rare;

		public string Name{ get { return this.name.ToString(); } }

		public abstract GameDefine.ItemType ItemType{ get; }
	}
}