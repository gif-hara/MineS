using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class EquipmentMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private int basePower;

		[SerializeField]
		private int brandingLimit;

		/// <summary>
		/// 抽出可能か.
		/// </summary>
		[SerializeField]
		private bool canExtraction;

		[SerializeField]
		private GameDefine.ItemType itemType;

		[SerializeField, EnumLabel("Ability", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> abilities;

		public int BasePower{ get { return this.basePower; } }

		public int BrandingLimit{ get { return this.brandingLimit; } }

		public bool CanExtraction{ get { return this.canExtraction; } }

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}


#if UNITY_EDITOR
		public static EquipmentMasterData CreateFromCsv(List<string> csv, GameDefine.ItemType itemType)
		{
			var result = CreateInstance<EquipmentMasterData>();
			result.itemType = itemType;
			result.id = int.Parse(csv[0]);
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/" + itemType.ToString() + "/" + itemType.ToString() + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.basePower = int.Parse(csv[5]);
			result.brandingLimit = int.Parse(csv[6]);
			result.canExtraction = bool.Parse(csv[7]);
			result.abilities = AbilityParser.Parse(csv[8]);

			return result;
		}
#endif
	}
}