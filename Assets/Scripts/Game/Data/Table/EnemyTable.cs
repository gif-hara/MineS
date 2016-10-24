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
	public class EnemyTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField]
			private List<EnemyCreateTable> enemies = new List<EnemyCreateTable>();

			public Element(Range floorRange)
				: base(floorRange)
			{
				
			}

			public EnemyData Create(CellController cellController)
			{
				var result = new EnemyData();
				var index = GameDefine.Lottery(this.enemies);
				result.Initialize(this.enemies[index].MasterData, cellController);
				return result;
			}
#if UNITY_EDITOR
			public void Add(string enemyName, int probability)
			{
				this.enemies.Add(EnemyCreateTable.Create(enemyName, probability));
			}
#endif
		}

		[SerializeField]
		private List<Element> elements = new List<Element>();

		public EnemyData Create(int floor, CellController cellController)
		{
#if DEBUG
			Debug.AssertFormat(this.elements.FindAll(e => e.IsMatch(floor)).Count == 1, "敵テーブルが無い、または複数ありました.");
#endif
			return this.elements.Find(e => e.IsMatch(floor)).Create(cellController);
		}

		public void Check(int floorMax)
		{
			var result = true;
			int floor = 1;
			Element element = this.elements.Find(e => e.IsMatch(floor));
			while(element != null || floor < floorMax)
			{
				var existCount = this.elements.FindAll(e => e.IsMatch(floor)).Count;
				result = result && existCount == 1;
				if(existCount == 0)
				{
					Debug.AssertFormat(false, "floor = {0}の敵テーブルがありません.", floor);
				}
				else if(existCount > 1)
				{
					Debug.AssertFormat(false, "floor = {0}に敵テーブルが複数存在しています.", floor);
				}
				floor++;
				element = this.elements.Find(e => e.IsMatch(floor));
			}
			floor--;

			if(floor != floorMax)
			{
				Debug.AssertFormat(false, "階数の最大値と一致していません. floor = {0} max = {1}", floor, floorMax);
				result = false;
			}

			if(result)
			{
				Debug.Log("敵テーブルに問題はありませんでした");
			}
		}

		[System.Serializable]
		private class EnemyCreateTable : IProbability
		{
			[SerializeField]
			private CharacterMasterData masterData;

			[SerializeField]
			private int probability;

			public CharacterMasterData MasterData{ get { return this.masterData; } }

			public int Probability{ get { return this.probability; } }

#if UNITY_EDITOR
			public static EnemyCreateTable Create(string enemyName, int probability)
			{
				var result = new EnemyCreateTable();
				result.masterData = EnemyList.Get(enemyName);
				result.probability = probability;

				return result;
			}
#endif
		}
#if UNITY_EDITOR
		public static EnemyTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}EnemyTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new EnemyTable();
			foreach(var c in csv)
			{
				var floorRange = new Range(int.Parse(c[0]), int.Parse(c[1]));
				var element = result.elements.Find(e => e.IsMatchRange(floorRange));
				if(element == null)
				{
					element = new Element(floorRange);
					result.elements.Add(element);
				}

				element.Add(c[2], int.Parse(c[3]));
			}

			return result;
		}
#endif
		
	}
}