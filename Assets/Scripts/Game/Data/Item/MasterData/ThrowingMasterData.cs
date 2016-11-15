using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class ThrowingMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private int power;

		[SerializeField]
		private int playerPower;

		[SerializeField]
		private bool canCoat;

		[SerializeField]
		private string description;

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.Arrow;
			}
		}
#if UNITY_EDITOR
		public static ThrowingMasterData CreateFromCsv(List<string> csv)
		{
			var result = CreateInstance<ThrowingMasterData>();
			result.id = int.Parse(csv[0]);
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/Throwing/Throwing" + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.power = int.Parse(csv[5]);
			result.playerPower = int.Parse(csv[6]);
			result.canCoat = bool.Parse(csv[7]);
			result.description = csv[8];
			return result;
		}
#endif

	}
}