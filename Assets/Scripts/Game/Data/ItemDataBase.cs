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
		protected int id;

		[SerializeField]
		protected string itemName;

		[SerializeField]
		protected int purchasePrice;

		[SerializeField]
		protected int sellingPrice;

		[SerializeField]
		protected Sprite image;

		public int Id{ get { return this.id; } }

		public string ItemName{ get { return this.itemName; } }

		public int PurchasePrice{ get { return this.purchasePrice; } }

		public int SellingPrice{ get { return this.sellingPrice; } }

		public Sprite Image{ get { return this.image; } }

		public abstract GameDefine.ItemType ItemType{ get; }
	}
}