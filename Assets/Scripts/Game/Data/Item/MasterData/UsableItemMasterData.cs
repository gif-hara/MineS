using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class UsableItemMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private GameDefine.UsableItemType type;

		[SerializeField]
		private int power0;

		[SerializeField]
		private int power1;

		[SerializeField]
		private bool canUnidentified;

		[SerializeField]
		private string description;

		[SerializeField]
		private AudioClip useSound;

		public GameDefine.UsableItemType UsableItemType{ get { return this.type; } }

		public int Power0{ get { return this.power0; } }

		public int Power1{ get { return this.power1; } }

		public bool CanUnidentified{ get { return this.canUnidentified; } }

		public AudioClip UseSound{ get { return this.useSound; } }

		public string Description
		{
			get
			{
				return this.description;
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.UsableItem;
			}
		}

#if UNITY_EDITOR
		public static UsableItemMasterData CreateFromCsv(List<string> csv)
		{
			var result = CreateInstance<UsableItemMasterData>();
			result.id = int.Parse(csv[0]);
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/UsableItem" + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.useSound = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/SE/UseItem" + csv[5] + ".mp3", typeof(AudioClip)) as AudioClip;
			result.type = GameDefine.GetUsableItemType(csv[6]);
			result.power0 = int.Parse(csv[7]);
			result.power1 = int.Parse(csv[8]);
			result.canUnidentified = bool.Parse(csv[9]);
			result.description = csv[10];
			return result;
		}
#endif
	}
}