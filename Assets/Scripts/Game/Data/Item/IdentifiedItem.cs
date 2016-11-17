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
	public class IdentifiedItem
	{
		[SerializeField]
		private ItemMasterDataBase item;

		[SerializeField]
		private string unidentifiedName;

		[SerializeField]
		private bool isIdentified;

		public bool IsIdentified{ get { return this.isIdentified; } }

		public IdentifiedItem()
		{
			this.item = null;
			this.unidentifiedName = "";
			this.isIdentified = false;
		}

		public IdentifiedItem(ItemMasterDataBase item, string unidentifiedName, bool isIdentified)
		{
			this.item = item;
			this.unidentifiedName = unidentifiedName;
			this.isIdentified = isIdentified;
		}

		public void Identified()
		{
			this.isIdentified = true;
		}

		public string ItemName
		{
			get
			{
				return this.IsIdentified ? this.item.ItemName : ItemManager.Instance.unidentifiedItemName.Element.Format(this.unidentifiedName);
			}
		}
	}
}