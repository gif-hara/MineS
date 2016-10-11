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
	public class EnemyMasterDataParser : MasterDataParserBase<CharacterMasterData>
	{
		[MenuItem("MineS/Parse EnemyMasterData")]
		private static void Parse()
		{
			Parse(
				csv => CharacterMasterData.CreateFromCsv(csv),
				"Assets/DataSources/Csv/EnemyMasterData.csv",
				"Assets/DataSources/Enemy/Enemy{0}.asset"
			);
		}
	}
}