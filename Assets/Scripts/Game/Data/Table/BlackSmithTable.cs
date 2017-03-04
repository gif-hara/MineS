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
	public class BlackSmithTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField][Range(0, 100)]
			private int probability;

			public Element(Range floorRange)
				: base(floorRange)
			{
				
			}

			public bool CanCreate
			{
				get
				{
                    var probability = this.probability + PlayerManager.Instance.Data.Inventory.Equipment.TotalLuck;
                    return probability > Random.Range(0, 100);
				}
			}

#if UNITY_EDITOR
			public void SetProbability(int value)
			{
				this.probability = value;
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
#if UNITY_EDITOR

		public void Check(int floorMax)
		{
			new TableChecker().Check(this.elements, typeof(BlackSmithTable), floorMax);
		}

		public static BlackSmithTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}BlackSmithTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new BlackSmithTable();
			foreach(var c in csv)
			{
				var floorRange = new Range(int.Parse(c[0]), int.Parse(c[1]));
				var element = new Element(floorRange);
				element.SetProbability(int.Parse(c[2]));
				result.elements.Add(element);
			}

			return result;
		}
#endif
		
	}
}