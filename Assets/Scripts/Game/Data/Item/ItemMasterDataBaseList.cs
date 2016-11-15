using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;
using System.IO;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu]
	public class ItemMasterDataBaseList : ScriptableObject
	{
		[SerializeField]
		private List<ItemMasterDataBase> database;

		public List<ItemMasterDataBase> Database{ get { return this.database; } }

#if UNITY_EDITOR
		public void SetDatabase(string itemType)
		{
			this.database = GetList(itemType);
		}

		public List<ItemMasterDataBase> Parse(string csvData)
		{
			var split = csvData.Split(' ');
			return database.Where(i => System.Array.FindIndex(split, s => s == i.ItemName) != -1).ToList();
		}

		private static Dictionary<string, ItemMasterDataBase> _allItem = null;

		public static ItemMasterDataBase Get(string itemName)
		{
			if(_allItem == null)
			{
				_allItem = new Dictionary<string, ItemMasterDataBase>();
				GetList("UsableItem").ForEach(i => _allItem.Add(i.ItemName, i));
				GetList("Weapon").ForEach(i => _allItem.Add(i.ItemName, i));
				GetList("Shield").ForEach(i => _allItem.Add(i.ItemName, i));
				GetList("Accessory").ForEach(i => _allItem.Add(i.ItemName, i));
			}

			Debug.AssertFormat(_allItem.ContainsKey(itemName), "{0}がありませんでした.", itemName);
			return _allItem[itemName];
		}

		private static List<ItemMasterDataBase> GetList(string itemType)
		{
			var result = new List<ItemMasterDataBase>();
			var format = string.Format("Assets/DataSources/Item/{0}/{0}{1}.asset", itemType, "{0}");
			int i = 0;
			var masterData = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemMasterDataBase>(string.Format(format, i));
			while(masterData != null)
			{
				result.Add(masterData);
				i++;
				masterData = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemMasterDataBase>(string.Format(format, i));
			}

			return result;
		}
#endif
	}
}