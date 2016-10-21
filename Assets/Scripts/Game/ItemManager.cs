using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ItemManager : SingletonMonoBehaviour<ItemManager>
	{
		public class IdentifiedItem
		{
			private ItemDataBase item;

			private string unidentifiedName;

			public bool IsIdentified{ private set; get; }

			public IdentifiedItem(ItemDataBase item, string unidentifiedName, bool isIdentified)
			{
				this.item = item;
				this.unidentifiedName = unidentifiedName;
				this.IsIdentified = isIdentified;
			}

			public void Identified()
			{
				this.IsIdentified = true;
			}

			public string ItemName
			{
				get
				{
					return this.IsIdentified ? this.item.ItemNameRaw : ItemManager.Instance.unidentifiedItemName.Element.Format(this.unidentifiedName);
				}
			}
		}

		[SerializeField]
		private ItemDataBaseList usableItemList;

		[SerializeField]
		private StringAsset unidentifiedStringAsset;

		public SerializeFieldGetter.StringAssetFinder unidentifiedDescription;

		public SerializeFieldGetter.StringAssetFinder equipmentRevisedLevelName;

		public SerializeFieldGetter.StringAssetFinder unidentifiedItemName;

		private Dictionary<string, IdentifiedItem> identifiedDictionary;

		private List<string> unidentifiedStrings;

		public void Initialize(DungeonDataBase dungeonData)
		{
			this.unidentifiedStrings = this.unidentifiedStringAsset.database.Select(d => d.value.Get("ja")).ToList();
			this.identifiedDictionary = new Dictionary<string, IdentifiedItem>();
			this.usableItemList.Database.ForEach(d =>
			{
				var unidentifiedStringIndex = Random.Range(0, this.unidentifiedStrings.Count);
				var identifiedItem = new IdentifiedItem(d, this.unidentifiedStrings[unidentifiedStringIndex], dungeonData.ItemIdentified && (d as UsableItemData).CanUnidentified);
				this.identifiedDictionary.Add(d.ItemNameRaw, identifiedItem);
				this.unidentifiedStrings.RemoveAt(unidentifiedStringIndex);
			});
		}

		public string GetItemName(ItemDataBase item)
		{
			Debug.AssertFormat(item.ItemType == GameDefine.ItemType.UsableItem, "{0}はUsableItemではありません.", item.ItemNameRaw);
			return this.identifiedDictionary[item.ItemNameRaw].ItemName;
		}

		public bool IsIdentified(ItemDataBase item)
		{
			Debug.AssertFormat(item.ItemType == GameDefine.ItemType.UsableItem, "{0}はUsableItemではありません.", item.ItemNameRaw);
			return this.identifiedDictionary[item.ItemNameRaw].IsIdentified;
		}

		public bool Identified(Item item)
		{
			Debug.AssertFormat(item.InstanceData.ItemType == GameDefine.ItemType.UsableItem, "{0}はUsableItemではありません.", item.InstanceData.ItemName);
			var identifiedItem = this.identifiedDictionary[item.InstanceData.ItemNameRaw];
			if(identifiedItem.IsIdentified)
			{
				return false;
			}

			identifiedItem.Identified();
			return true;
		}
	}
}