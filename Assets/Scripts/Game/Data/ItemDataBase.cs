using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public abstract class ItemDataBase : ScriptableObject
	{
		[SerializeField]
		protected StringAsset.Finder itemName;

		[SerializeField]
		protected GameDefine.RareType rare;

		public string ItemName{ get { return this.itemName.ToString(); } }

		public abstract GameDefine.ItemType ItemType{ get; }

		public abstract ItemDataBase Clone{ get; }

		protected void InternalClone(ItemDataBase clone)
		{
			clone.itemName = this.itemName;
			clone.rare = this.rare;
		}
	}
}