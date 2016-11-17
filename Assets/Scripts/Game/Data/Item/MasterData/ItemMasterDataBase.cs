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
	public abstract class ItemMasterDataBase : ScriptableObject
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

		/// <summary>
		/// 未識別可能か返す.
		/// </summary>
		/// <value><c>true</c> if this instance can unidentified; otherwise, <c>false</c>.</value>
		public virtual bool CanUnidentified
		{
			get
			{
				return false;
			}
		}
	}
}