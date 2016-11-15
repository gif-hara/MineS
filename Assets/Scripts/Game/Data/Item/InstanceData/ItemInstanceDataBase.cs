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
		protected int id;

		[SerializeField]
		protected string itemName;

		[SerializeField]
		protected int purchasePrice;

		[SerializeField]
		protected int sellingPrice;

		[SerializeField]
		protected Sprite image;

		[SerializeField]
		private ItemMasterDataBase masterData;

		public ItemMasterDataBase MasterData{ get { return this.masterData; } }

		public int Id{ get { return this.id; } }

		public virtual string ItemName{ get { return this.itemName.ToString(); } }

		public string ItemNameRaw{ get { return this.itemName.ToString(); } }

		public virtual int PurchasePrice{ get { return this.purchasePrice; } }

		public virtual int SellingPrice{ get { return this.sellingPrice; } }

		public Sprite Image{ get { return this.image; } }

		public abstract GameDefine.ItemType ItemType{ get; }

		protected void InternalCreateFromMasterData(ItemInstanceDataBase instanceData, ItemMasterDataBase masterData)
		{
			instanceData.id = masterData.Id;
			instanceData.itemName = masterData.ItemName;
			instanceData.purchasePrice = masterData.PurchasePrice;
			instanceData.sellingPrice = masterData.SellingPrice;
			instanceData.image = masterData.Image;
			instanceData.masterData = masterData;
		}
	}
}