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
	public class ShopTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			public Element(Range floorRange)
				: base(floorRange)
			{
				
			}

			[SerializeField][Range(0, 100)]
			private int probability;

			[SerializeField]
			private ItemTable itemTable = new ItemTable();

			public Inventory CreateInventory()
			{
				var result = new Inventory(null, GameDefine.ShopInventoryMax);
				for(int i = 0; i < GameDefine.ShopInventoryMax; i++)
				{
					result.AddItemNoLimit(this.itemTable.Create());
				}

				return result;
			}

			public bool CanCreate
			{
				get
				{
					return this.probability > Random.Range(0, 100);
				}
			}

#if UNITY_EDITOR
			public void SetProbability(int value)
			{
				this.probability = value;
			}

			public void Add(string itemName, int probability)
			{
				this.itemTable.Add(itemName, probability);
			}
#endif
		}

		[SerializeField]
		private List<Element> elements = new List<Element>();

		public bool CanCreate(int floor)
		{
			var element = this.elements.Find(e => e.IsMatch(floor));
			if(element == null)
			{
				return false;
			}

			return element.CanCreate;
		}

		public Inventory CreateInventory(int floor)
		{
			var element = this.elements.Find(e => e.IsMatch(floor));
			Debug.AssertFormat(element != null, "店データが無いのに道具袋を作ろうとしました. floor = {0}", floor);

			return element.CreateInventory();
		}

#if UNITY_EDITOR
		public static ShopTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}ShopTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new ShopTable();
			foreach(var c in csv)
			{
				var floorRange = new Range(int.Parse(c[0]), int.Parse(c[1]));
				var element = result.elements.Find(e => e.IsMatchRange(floorRange));
				if(element == null)
				{
					element = new Element(floorRange);
					element.SetProbability(int.Parse(c[2]));
					result.elements.Add(element);
				}
				element.Add(c[3], int.Parse(c[4]));
			}

			return result;
		}
#endif
	}
}