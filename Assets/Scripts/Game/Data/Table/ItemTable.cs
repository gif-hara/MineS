using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class ItemTable
	{
		[SerializeField]
		private List<Element> elements = new List<Element>();

		public Item Create()
		{
			return this.elements[GameDefine.Lottery(this.elements)].Create();
		}
#if UNITY_EDITOR
		public void Add(string itemName, int probability)
		{
			this.elements.Add(Element.Create(itemName, probability));
		}

		public static ItemTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}ItemTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new ItemTable();
			foreach(var c in csv)
			{
				result.elements.Add(Element.Create(c[0], int.Parse(c[1])));
			}

			return result;
		}
#endif

		[System.Serializable]
		private class Element : IProbability
		{
			[SerializeField]
			private ItemMasterDataBase masterData;

			[SerializeField]
			private int probability;

			public ItemMasterDataBase MasterData{ get { return this.masterData; } }

			public int Probability{ get { return this.probability; } }

			public Item Create()
			{
				return new Item(this.masterData);
			}
#if UNITY_EDITOR
			public static Element Create(string itemName, int probability)
			{
				var result = new Element();
				result.masterData = ItemMasterDataBaseList.Get(itemName);
				result.probability = probability;

				return result;
			}
#endif
		}

	}
}