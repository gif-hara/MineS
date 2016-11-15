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

		[SerializeField]
		private ItemMasterDataBaseList usableItemList;

		[SerializeField]
		private StringAsset unidentifiedStringAsset;

		public SerializeFieldGetter.StringAssetFinder unidentifiedDescription;

		public SerializeFieldGetter.StringAssetFinder equipmentRevisedLevelName;

		public SerializeFieldGetter.StringAssetFinder unidentifiedItemName;

		private Dictionary<string, IdentifiedItem> identifiedDictionary = new Dictionary<string, IdentifiedItem>();

		private const string DictionaryCountSerializeKeyName = "ItemManagerIdentifiedDictionaryCount";

		void Start()
		{
			DungeonManager.Instance.AddChangeDungeonEvent(this.Initialize);
		}

		public void Initialize()
		{
			var unidentifiedStrings = this.unidentifiedStringAsset.database.Select(d => d.value.Get("ja")).ToList();
			this.identifiedDictionary = new Dictionary<string, IdentifiedItem>();
			this.usableItemList.Database.ForEach(d =>
			{
				var unidentifiedStringIndex = Random.Range(0, unidentifiedStrings.Count);
				var idenditied = !(d as UsableItemMasterData).CanUnidentified ? true : DungeonManager.Instance.CurrentData.ItemIdentified;
				var identifiedItem = new IdentifiedItem(d, unidentifiedStrings[unidentifiedStringIndex], idenditied);
				this.identifiedDictionary.Add(d.ItemName, identifiedItem);
				unidentifiedStrings.RemoveAt(unidentifiedStringIndex);
			});
		}

		public string GetItemName(ItemInstanceDataBase item)
		{
			if(!this.identifiedDictionary.ContainsKey(item.ItemNameRaw))
			{
				return item.ItemNameRaw;
			}
			Debug.AssertFormat(item.ItemType == GameDefine.ItemType.UsableItem, "{0}はUsableItemではありません.", item.ItemNameRaw);
			return this.identifiedDictionary[item.ItemNameRaw].ItemName;
		}

		public bool IsIdentified(ItemInstanceDataBase item)
		{
			if(!this.identifiedDictionary.ContainsKey(item.ItemNameRaw))
			{
				return true;
			}
			Debug.AssertFormat(item.ItemType == GameDefine.ItemType.UsableItem, "{0}はUsableItemではありません.", item.ItemNameRaw);
			return this.identifiedDictionary[item.ItemNameRaw].IsIdentified;
		}

		public bool Identified(Item item)
		{
			if(!this.identifiedDictionary.ContainsKey(item.InstanceData.ItemNameRaw))
			{
				return false;
			}
			Debug.AssertFormat(item.InstanceData.ItemType == GameDefine.ItemType.UsableItem, "{0}はUsableItemではありません.", item.InstanceData.ItemName);
			var identifiedItem = this.identifiedDictionary[item.InstanceData.ItemNameRaw];
			if(identifiedItem.IsIdentified)
			{
				return false;
			}

			identifiedItem.Identified();
			return true;
		}

		public void Serialize()
		{
			int count = this.identifiedDictionary.Count;
			HK.Framework.SaveData.SetInt(DictionaryCountSerializeKeyName, count);
			int i = 0;
			foreach(var d in this.identifiedDictionary)
			{
				HK.Framework.SaveData.SetString(this.GetDictionaryKeySerializeKeyName(i), d.Key);
				HK.Framework.SaveData.SetClass<IdentifiedItem>(this.GetDictionaryValueSerializeKeyName(i), d.Value);
				i++;
			}
		}

		public void Deserialize()
		{
			if(!HK.Framework.SaveData.ContainsKey(DictionaryCountSerializeKeyName))
			{
				return;
			}

			this.identifiedDictionary = new Dictionary<string, IdentifiedItem>();
			var count = HK.Framework.SaveData.GetInt(DictionaryCountSerializeKeyName);
			for(int i = 0; i < count; i++)
			{
				this.identifiedDictionary.Add(
					HK.Framework.SaveData.GetString(this.GetDictionaryKeySerializeKeyName(i)),
					HK.Framework.SaveData.GetClass<IdentifiedItem>(this.GetDictionaryValueSerializeKeyName(i), null)
				);
			}
		}

		private string GetDictionaryKeySerializeKeyName(int index)
		{
			return string.Format("ItemManagerIdentifiedDictionaryKey_{0}", index);
		}

		private string GetDictionaryValueSerializeKeyName(int index)
		{
			return string.Format("ItemManagerIdentifiedDictionaryValue_{0}", index);
		}
	}
}