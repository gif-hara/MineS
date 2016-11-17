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
	[System.Serializable]
	public class IdentifiedItemManager
	{
		[SerializeField]
		private Dictionary<string, IdentifiedItem> dictionary = new Dictionary<string, IdentifiedItem>();

		public IdentifiedItemManager(ItemMasterDataBaseList list, StringAsset unidentifiedStringAsset)
		{
			Debug.AssertFormat(list.Database.Count <= unidentifiedStringAsset.database.Count, "未識別文字列が足りません. list = {0}", list.name);
			var unidentifiedStrings = unidentifiedStringAsset.database.Select(d => d.value.Get()).ToList();
			list.Database.ForEach(d =>
			{
				var unidentifiedStringIndex = Random.Range(0, unidentifiedStrings.Count);
				var idenditied = !d.CanUnidentified ? true : DungeonManager.Instance.CurrentData.ItemIdentified;
				var identifiedItem = new IdentifiedItem(d, unidentifiedStrings[unidentifiedStringIndex], idenditied);
				this.dictionary.Add(d.ItemName, identifiedItem);
				unidentifiedStrings.RemoveAt(unidentifiedStringIndex);
			});
		}

		public string Get(ItemInstanceDataBase instanceData)
		{
			if(!this.dictionary.ContainsKey(instanceData.ItemNameRaw))
			{
				return instanceData.ItemNameRaw;
			}

			return this.dictionary[instanceData.ItemNameRaw].ItemName;
		}

		public bool IsIdentified(ItemInstanceDataBase instanceData)
		{
			if(!this.dictionary.ContainsKey(instanceData.ItemNameRaw))
			{
				return true;
			}

			return this.dictionary[instanceData.ItemNameRaw].IsIdentified;
		}

		public bool Identified(Item item)
		{
			if(!this.dictionary.ContainsKey(item.InstanceData.ItemNameRaw))
			{
				return false;
			}

			var identifiedItem = this.dictionary[item.InstanceData.ItemNameRaw];
			if(identifiedItem.IsIdentified)
			{
				return false;
			}

			identifiedItem.Identified();
			return true;
		}

		public void Serialize(string key)
		{
			int count = this.dictionary.Count;
			HK.Framework.SaveData.SetInt(this.GetSerializeDictionaryCountName(key), count);
			int i = 0;
			foreach(var d in this.dictionary)
			{
				HK.Framework.SaveData.SetString(this.GetSerializeKeyName(key, i), d.Key);
				HK.Framework.SaveData.SetClass<IdentifiedItem>(this.GetSerializeValueName(key, i), d.Value);
				i++;
			}
		}

		public void Deserialize(string key)
		{
			if(!HK.Framework.SaveData.ContainsKey(this.GetSerializeDictionaryCountName(key)))
			{
				return;
			}

			this.dictionary = new Dictionary<string, IdentifiedItem>();
			var count = HK.Framework.SaveData.GetInt(this.GetSerializeDictionaryCountName(key));
			for(int i = 0; i < count; i++)
			{
				this.dictionary.Add(
					HK.Framework.SaveData.GetString(this.GetSerializeKeyName(key, i)),
					HK.Framework.SaveData.GetClass<IdentifiedItem>(this.GetSerializeValueName(key, i), null)
				);
			}
		}

		private string GetSerializeDictionaryCountName(string key)
		{
			return string.Format("{0}_Count", key);
		}

		private string GetSerializeKeyName(string key, int index)
		{
			return string.Format("{0}_Key_{1}", key, index);
		}

		private string GetSerializeValueName(string key, int index)
		{
			return string.Format("{0}_Value_{1}", key, index);
		}
	}
}