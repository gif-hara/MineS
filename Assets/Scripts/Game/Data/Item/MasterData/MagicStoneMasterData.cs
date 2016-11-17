using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class MagicStoneMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private GameDefine.MagicStoneType type;

		[SerializeField]
		private int addPurchasePrice;

		[SerializeField]
		private int addSellingPrice;

		[SerializeField]
		private string description;

		public GameDefine.MagicStoneType MagicStoneType{ get { return this.type; } }

		public int AddPurchasePrice{ get { return this.addPurchasePrice; } }

		public int AddSellingPrice{ get { return this.addSellingPrice; } }

		public string Description{ get { return this.description; } }

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.MagicStone;
			}
		}

		public override bool CanUnidentified
		{
			get
			{
				return true;
			}
		}

#if UNITY_EDITOR
		public static MagicStoneMasterData CreateFromCsv(List<string> csv)
		{
			var result = CreateInstance<MagicStoneMasterData>();
			result.id = int.Parse(csv[0]);
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/MagicStone/MagicStone" + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.type = GameDefine.GetType<GameDefine.MagicStoneType>(csv[5]);
			result.addPurchasePrice = int.Parse(csv[6]);
			result.addSellingPrice = int.Parse(csv[7]);
			result.description = csv[8];
			return result;
		}
#endif
		
	}
}