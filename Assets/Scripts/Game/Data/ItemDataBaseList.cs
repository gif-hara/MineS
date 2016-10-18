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
		[ContextMenu("Set as UsableItemList")]
		private void SetAsUsableItem()
		{
		}

		public List<ItemDataBase> Parse(string csvData)
		{
			var split = csvData.Split(' ');
			return database.Where(i => System.Array.FindIndex(split, s => s == i.ItemName) != -1).ToList();
		}
#endif
	}
}