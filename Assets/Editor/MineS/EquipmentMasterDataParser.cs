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
	public class EquipmentMasterDataParser : MasterDataParserBase<EquipmentData>
	{
		[MenuItem("MineS/Parse Weapon MasterData")]
		private static void Parse()
		{
			Parse(
				csv => EquipmentData.CreateFromCsv(csv, GameDefine.ItemType.Weapon),
				"Assets/DataSources/Csv/WeaponMasterData.csv",
				"Assets/DataSources/Item/Weapon/Weapon{0}.asset"
			);
		}
	}
}