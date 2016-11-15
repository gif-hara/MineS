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
	public class UsableItemInstanceData : ItemInstanceDataBase
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

		public UsableItemInstanceData(ItemMasterDataBase masterData)
		{
			base.InternalCreateFromMasterData(this, masterData);
			var usableItemMasterData = masterData as UsableItemMasterData;
			this.type = usableItemMasterData.UsableItemType;
			this.power0 = usableItemMasterData.Power0;
			this.power1 = usableItemMasterData.Power1;
			this.canUnidentified = usableItemMasterData.CanUnidentified;
			this.description = usableItemMasterData.Description;
		}

		public UsableItemInstanceData()
		{
			
		}

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
	}
}