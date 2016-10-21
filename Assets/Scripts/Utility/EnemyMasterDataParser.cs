#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEditor;
using System.Linq;
using System.IO;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyMasterDataParser : MasterDataParserBase<CharacterMasterData>
	{
		[MenuItem("MineS/MasterData/Parse/Enemy")]
		private static void Parse()
		{
			Parse(
				csv => CharacterMasterData.CreateFromCsv(csv),
				"Assets/DataSources/Csv/EnemyMasterData.csv",
				"Assets/DataSources/Enemy/Enemy{0}.asset"
			);

			int i = 0;
			var masterData = AssetDatabase.LoadAssetAtPath<CharacterMasterData>(string.Format("Assets/DataSources/Enemy/Enemy{0}.asset", i));
			while(masterData != null)
			{
				masterData.SetLevelData();
				i++;
				masterData = AssetDatabase.LoadAssetAtPath<CharacterMasterData>(string.Format("Assets/DataSources/Enemy/Enemy{0}.asset", i));
			}
		}
	}
}
#endif