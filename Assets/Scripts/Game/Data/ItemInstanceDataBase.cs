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
	public abstract class ItemInstanceDataBase
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

		protected void InternalCreateFromMasterData(ItemInstanceDataBase instanceData, ItemDataBase masterData)
		{
			instanceData.itemName = masterData.ItemName;
			instanceData.purchasePrice = masterData.PurchasePrice;
			instanceData.sellingPrice = masterData.SellingPrice;
			instanceData.image = masterData.Image;
		}
	}
}