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
	public class ThrowingMasterDataParser : MasterDataParserBase<ThrowingMasterData>
	{
		[MenuItem("MineS/MasterData/Parse/ThrowingItem")]
		private static void Parse()
		{
			Parse(
				csv => ThrowingMasterData.CreateFromCsv(csv),
				"Assets/DataSources/Csv/ThrowingMasterData.csv",
				"Assets/DataSources/Item/Throwing/Throwing{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/Throwing.asset").SetDatabase("Throwing");
		}
	}
}
#endif