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
	public class UsableItemMasterDataParser : MasterDataParserBase<UsableItemMasterData>
	{
		[MenuItem("MineS/MasterData/Parse/UsableItem")]
		private static void Parse()
		{
			Parse(
				csv => UsableItemMasterData.CreateFromCsv(csv),
				"Assets/DataSources/Csv/UsableItemMasterData.csv",
				"Assets/DataSources/Item/UsableItem/UsableItem{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/UsableItem.asset").SetDatabase("UsableItem");
		}
	}
}
#endif