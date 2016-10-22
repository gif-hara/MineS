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
		protected string itemName;

		[SerializeField]
		protected int purchasePrice;

		[SerializeField]
		protected int sellingPrice;

		[SerializeField]
		protected Sprite image;

		public ItemDataBase MasterData{ private set; get; }

		public virtual string ItemName{ get { return this.itemName.ToString(); } }

		public string ItemNameRaw{ get { return this.itemName.ToString(); } }

		public int PurchasePrice{ get { return this.purchasePrice; } }

		public int SellingPrice{ get { return this.sellingPrice; } }

		public Sprite Image{ get { return this.image; } }

		public abstract GameDefine.ItemType ItemType{ get; }

		public abstract ItemDataBase Clone{ get; }

		protected void InternalClone(ItemDataBase clone)
		{
			clone.itemName = this.itemName;
			clone.purchasePrice = this.purchasePrice;
			clone.sellingPrice = this.sellingPrice;
			clone.image = this.image;
			clone.MasterData = this;
		}
	}
}