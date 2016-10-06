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

		[SerializeField]
		protected int purchasePrice;

		[SerializeField]
		protected int sellingPrice;

		[SerializeField]
		private Sprite image;

		public virtual string ItemName{ get { return this.itemName.ToString(); } }

		public GameDefine.RareType RareType{ get { return this.rare; } }

		public Sprite Image{ get { return this.image; } }

		public abstract GameDefine.ItemType ItemType{ get; }

		public abstract ItemDataBase Clone{ get; }

		protected void InternalClone(ItemDataBase clone)
		{
			clone.itemName = this.itemName;
			clone.rare = this.rare;
			clone.purchasePrice = this.purchasePrice;
			clone.sellingPrice = this.sellingPrice;
			clone.image = this.image;
		}
	}
}