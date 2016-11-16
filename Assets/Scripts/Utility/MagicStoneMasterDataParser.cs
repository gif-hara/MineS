#if UNITY_EDITOR
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
	public class MagicStoneMasterDataParser : MasterDataParserBase<MagicStoneMasterData>
	{
		[MenuItem("MineS/MasterData/Parse/MagicStone")]
		private static void Parse()
		{
			Parse(
				csv => MagicStoneMasterData.CreateFromCsv(csv),
				"Assets/DataSources/Csv/MagicStoneMasterData.csv",
				"Assets/DataSources/Item/MagicStone/MagicStone{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/MagicStone.asset").SetDatabase("MagicStone");
		}
	}
}
#endif