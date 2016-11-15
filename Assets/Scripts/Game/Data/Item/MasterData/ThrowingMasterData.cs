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
		private GameDefine.ThrowingType type;

		[SerializeField]
		private int power;

		[SerializeField]
		private int playerPower;

		[SerializeField]
		private string description;

		public GameDefine.ThrowingType ThrowingType{ get { return this.type; } }

		public int Power{ get { return this.power; } }

		public int PlayerPower{ get { return this.playerPower; } }

		public string Description{ get { return this.description; } }

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.Throwing;
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
			result.type = GameDefine.GetType<GameDefine.ThrowingType>(csv[5]);
			result.power = int.Parse(csv[6]);
			result.playerPower = int.Parse(csv[7]);
			result.description = csv[8];
			return result;
		}
#endif

	}
}