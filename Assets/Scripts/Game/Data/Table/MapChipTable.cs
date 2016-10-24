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
	public class MapChipTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField]
			private MapChipData mapChip;

			public Element(Range floorRange)
				: base(floorRange)
			{
			}

#if UNITY_EDITOR
			public void SetMapChip(string mapChipName)
			{
				this.mapChip = AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/MapChip/{0}.asset", mapChipName), typeof(MapChipData)) as MapChipData;
			}
#endif

			public MapChipData MapChip{ get { return this.mapChip; } }
		}

		[SerializeField]
		private List<Element> elements = new List<Element>();

		public MapChipData Get(int floor)
		{
			return this.elements.Find(e => e.IsMatch(floor)).MapChip;
		}

#if UNITY_EDITOR
		public static MapChipTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}MapChipTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new MapChipTable();
			foreach(var c in csv)
			{
				var floorRange = new Range(int.Parse(c[0]), int.Parse(c[1]));
				var element = result.elements.Find(e => e.IsMatchRange(floorRange));
				if(element == null)
				{
					element = new Element(floorRange);
					result.elements.Add(element);
				}
				element.SetMapChip(c[2]);
			}

			return result;
		}
#endif
	}
}