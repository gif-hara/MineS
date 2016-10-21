﻿using UnityEngine;
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
	public class UsableItemData : ItemDataBase
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

		public override string ItemName
		{
			get
			{
				return ItemManager.Instance.GetItemName(this);
			}
		}

		public GameDefine.UsableItemType UsableItemType{ get { return this.type; } }

		public int Power0{ get { return this.power0; } }

		public int Power1{ get { return this.power1; } }

		public bool CanUnidentified{ get { return this.canUnidentified; } }

		public int RandomPower{ get { return Random.Range(this.power0, this.power1 + 1); } }

		public DescriptionData.Element DescriptionElement{ get { return new DescriptionData.Element(this.ItemName, this.Description, this.Image); } }

		private string Description
		{
			get
			{
				return ItemManager.Instance.IsIdentified(this)
					? this.description
						: ItemManager.Instance.unidentifiedDescription.Element.Get;
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.UsableItem;
			}
		}

		public override ItemDataBase Clone
		{
			get
			{
				var result = ScriptableObject.CreateInstance<UsableItemData>();
				this.InternalClone(result);
				result.type = this.type;
				result.power0 = this.power0;
				result.power1 = this.power1;
				result.canUnidentified = this.canUnidentified;
				result.description = this.description;

				return result;
			}
		}

#if UNITY_EDITOR
		public static UsableItemData CreateFromCsv(List<string> csv)
		{
			var result = CreateInstance<UsableItemData>();
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/UsableItem" + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.type = GameDefine.GetUsableItemType(csv[5]);
			result.power0 = int.Parse(csv[6]);
			result.power1 = int.Parse(csv[7]);
			result.canUnidentified = bool.Parse(csv[8]);
			result.description = csv[9];
			return result;
		}
#endif
	}
}