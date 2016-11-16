using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class MagicStoneInstanceData : ItemInstanceDataBase
	{
		[SerializeField]
		private GameDefine.MagicStoneType type;

		[SerializeField]
		private int addPurchasePrice;

		[SerializeField]
		private int addSellingPrice;

		[SerializeField]
		private string description;

		/// <summary>
		/// 残弾数.
		/// </summary>
		[SerializeField]
		private int remainingNumber;

		public GameDefine.MagicStoneType MagicStoneType{ get { return this.type; } }

		public DescriptionData.Element DescriptionElement{ get { return new DescriptionData.Element(this.ItemName, this.description, this.Image); } }

		public int RemainingNumber{ get { return this.remainingNumber; } }

		public bool IsEmpty{ get { return this.remainingNumber <= 0; } }

		public MagicStoneInstanceData(ItemMasterDataBase masterData, int remainingNumber)
		{
			base.InternalCreateFromMasterData(this, masterData);
			var magicStoneMasterData = masterData as MagicStoneMasterData;
			this.type = magicStoneMasterData.MagicStoneType;
			this.addPurchasePrice = magicStoneMasterData.AddPurchasePrice;
			this.addSellingPrice = magicStoneMasterData.AddSellingPrice;
			this.description = magicStoneMasterData.Description;
			this.remainingNumber = remainingNumber;
		}

		public MagicStoneInstanceData()
		{
			
		}

		public override string ItemName
		{
			get
			{
				return ItemManager.Instance.magicStoneItemReminaingName.Element.Format(this.itemName, this.remainingNumber);
			}
		}

		public override int PurchasePrice
		{
			get
			{
				return this.purchasePrice + this.addPurchasePrice * this.remainingNumber;
			}
		}

		public override int SellingPrice
		{
			get
			{
				return this.sellingPrice + this.addSellingPrice * this.remainingNumber;
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.MagicStone;
			}
		}

		public void Add(int value)
		{
			this.remainingNumber += value;
			this.remainingNumber = this.remainingNumber > GameDefine.MagicStoneItemMax ? GameDefine.MagicStoneItemMax : this.remainingNumber;
		}

		public void Use(CharacterData attacker, IAttack target)
		{
			this.remainingNumber--;
			switch(this.type)
			{
			case GameDefine.MagicStoneType.AddDebuff_Dull:
				this.AddAbnormalStatus(GameDefine.AbnormalStatusType.Dull, target);
			break;
			case GameDefine.MagicStoneType.AddDebuff_Gout:
				this.AddAbnormalStatus(GameDefine.AbnormalStatusType.Gout, target);
			break;
			case GameDefine.MagicStoneType.AddDebuff_Fear:
				this.AddAbnormalStatus(GameDefine.AbnormalStatusType.Fear, target);
			break;
			case GameDefine.MagicStoneType.AddDebuff_Seal:
				this.AddAbnormalStatus(GameDefine.AbnormalStatusType.Seal, target);
			break;
			case GameDefine.MagicStoneType.AddDebuff_Confusion:
				this.AddAbnormalStatus(GameDefine.AbnormalStatusType.Confusion, target);
			break;
			case GameDefine.MagicStoneType.AddDebuff_Assumption:
				this.AddAbnormalStatus(GameDefine.AbnormalStatusType.Assumption, target);
			break;
			case GameDefine.MagicStoneType.AddRandomAbnormalStatus:
				this.AddAbnormalStatus(GameDefine.RandomAbnormalStatus, target);
			break;
			case GameDefine.MagicStoneType.ChangeSlug:
				target.ChangeMasterData(EnemyManager.Instance.SlugMasterData);
			break;
			case GameDefine.MagicStoneType.LevelUp:
				target.ForceLevelUp(1);
			break;
			case GameDefine.MagicStoneType.LevelDown:
				target.ForceLevelDown(1);
			break;
			}
		}

		private void AddAbnormalStatus(GameDefine.AbnormalStatusType abnormalStatusType, IAttack target)
		{
			var addResult = target.AddAbnormalStatus(AbnormalStatusFactory.Create(abnormalStatusType, target, GameDefine.AddMagicStoneAbnormalStatusTurn, 0));
			InformationManager.OnUseAddAbnormalStatusItem(target, abnormalStatusType, addResult);
		}
	}
}