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
	public class EquipmentMasterDataParser : MasterDataParserBase<EquipmentMasterData>
	{
		[MenuItem("MineS/MasterData/Parse/Weapon")]
		private static void ParseWeaponMasterData()
		{
			Parse(
				csv => EquipmentMasterData.CreateFromCsv(csv, GameDefine.ItemType.Weapon),
				"Assets/DataSources/Csv/WeaponMasterData.csv",
				"Assets/DataSources/Item/Weapon/Weapon{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/Weapon.asset").SetDatabase("Weapon");
		}

		[MenuItem("MineS/MasterData/Parse/Shield")]
		private static void ParseShieldMasterData()
		{
			Parse(
				csv => EquipmentMasterData.CreateFromCsv(csv, GameDefine.ItemType.Shield),
				"Assets/DataSources/Csv/ShieldMasterData.csv",
				"Assets/DataSources/Item/Shield/Shield{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/Shield.asset").SetDatabase("Shield");
		}

		[MenuItem("MineS/MasterData/Parse/Accessory")]
		private static void ParseAccessoryMasterData()
		{
			Parse(
				csv => EquipmentMasterData.CreateFromCsv(csv, GameDefine.ItemType.Accessory),
				"Assets/DataSources/Csv/AccessoryMasterData.csv",
				"Assets/DataSources/Item/Accessory/Accessory{0}.asset"
			);
			AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/Accessory.asset").SetDatabase("Accessory");
		}
	}
}
#endif