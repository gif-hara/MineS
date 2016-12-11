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
	public class StoneStatueTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField]
			private List<CreateTable> createTable = new List<CreateTable>();

			[SerializeField][Range(0, 100)]
			private int probability;

			public int Probability{ get { return this.probability; } }

			public Element(Range floorRange)
				: base(floorRange)
			{
				
			}

			public GameDefine.StoneStatueType GetCreateType()
			{
				return this.createTable[GameDefine.Lottery(this.createTable)].Type;
			}

			public void SetProbability(int probability)
			{
				this.probability = probability;
			}

			public void Add(CreateTable table)
			{
				this.createTable.Add(table);
			}

			public bool CanCreate
			{
				get
				{
					return this.probability > Random.Range(0, 100);
				}
			}
		}

		[System.Serializable]
		public class CreateTable : IProbability
		{
			[SerializeField]
			private GameDefine.StoneStatueType type;

			[SerializeField]
			private int probability;

			public GameDefine.StoneStatueType Type{ get { return this.type; } }

			public int Probability{ get { return this.probability; } }

#if UNITY_EDITOR
			public static CreateTable Create(GameDefine.StoneStatueType type, int probability)
			{
				var result = new CreateTable();
				result.type = type;
				result.probability = probability;

				return result;
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

		public GameDefine.StoneStatueType GetCreateType(int floor)
		{
			var element = this.elements.Find(e => e.IsMatch(floor));
			return element.GetCreateType();
		}
#if UNITY_EDITOR
		public static StoneStatueTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}StoneStatueTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new StoneStatueTable();
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

				element.Add(CreateTable.Create(GameDefine.GetType<GameDefine.StoneStatueType>(c[3]), int.Parse(c[4])));
			}

			return result;
		}
#endif

	}
}