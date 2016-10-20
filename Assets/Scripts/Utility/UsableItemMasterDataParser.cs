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
	public class UsableItemMasterDataParser : MasterDataParserBase<UsableItemData>
	{
		[MenuItem("MineS/Parse UsableItem MasterData")]
		private static void Parse()
		{
			Parse(
				csv => UsableItemData.CreateFromCsv(csv),
				"Assets/DataSources/Csv/UsableItemMasterData.csv",
				"Assets/DataSources/Item/UsableItem/UsableItem{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemDataBaseList>("Assets/DataSources/Item/ItemList/UsableItem.asset").SetDatabase("UsableItem");
		}
	}
}
#endif