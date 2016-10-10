using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEditor;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyMasterDataPaser
	{
		[MenuItem("MineS/Parse EnemyMasterData")]
		private static void Parse()
		{
			var csvData = AssetDatabase.LoadAssetAtPath("Assets/DataSources/Csv/EnemyMasterData.csv", typeof(TextAsset)) as TextAsset;
			var list = CsvParser<CharacterMasterData>.Parse(csvData, csv => CharacterMasterData.CreateFromCsv(csv));
			foreach(var item in list.Select((v, i) => new {v, i}))
			{
				EditorUtility.DisplayProgressBar("Create EnemyMasterData", string.Format("Creating... {0}/{1}", item.i, list.Count), (float)item.i / list.Count);
				AssetDatabase.CreateAsset(item.v, "Assets/DataSources/Enemy/Enemy" + item.i + ".asset");
			}

			EditorUtility.ClearProgressBar();
		}
	}
}