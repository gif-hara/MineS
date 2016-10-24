using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class TrapTable
	{
		[SerializeField]
		private List<Element> elements = new List<Element>();

		public InvokeTrapActionBase Create()
		{
			return this.elements[GameDefine.Lottery(this.elements)].Create();
		}

#if UNITY_EDITOR
		public static TrapTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}TrapTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new TrapTable();
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
			private GameDefine.TrapType type;

			[SerializeField]
			private int probability;

			public int Probability{ get { return this.probability; } }

			public InvokeTrapActionBase Create()
			{
				return InvokeTrapFactory.Create(this.type);
			}

#if UNITY_EDITOR
			public static Element Create(string typeName, int probability)
			{
				var result = new Element();
				result.type = GameDefine.GetTrapType(typeName);
				result.probability = probability;

				return result;
			}
#endif
		}

	}
}