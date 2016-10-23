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
	public class ItemDataBaseList : ScriptableObject
	{
		[SerializeField]
		private List<ItemDataBase> database;

		public List<ItemDataBase> Database{ get { return this.database; } }

#if UNITY_EDITOR
		public void SetDatabase(string itemType)
		{
			this.database = new List<ItemDataBase>();
			var format = string.Format("Assets/DataSources/Item/{0}/{0}{1}.asset", itemType, "{0}");
			int i = 0;
			var masterData = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemDataBase>(string.Format(format, i));
			while(masterData != null)
			{
				this.database.Add(masterData);
				i++;
				masterData = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemDataBase>(string.Format(format, i));
			}
		}

		public List<ItemDataBase> Parse(string csvData)
		{
			var split = csvData.Split(' ');
			return database.Where(i => System.Array.FindIndex(split, s => s == i.ItemNameRaw) != -1).ToList();
		}
#endif
	}
}