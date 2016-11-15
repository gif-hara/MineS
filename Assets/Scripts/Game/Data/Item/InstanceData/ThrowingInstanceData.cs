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
	public class ThrowingInstanceData : ItemInstanceDataBase
	{
		[SerializeField]
		private GameDefine.ThrowingType type;

		[SerializeField]
		private int power;

		[SerializeField]
		private int playerPower;

		[SerializeField]
		private string description;

		/// <summary>
		/// 残弾数.
		/// </summary>
		[SerializeField]
		private int remainingNumber;

		/// <summary>
		/// 塗布したポーションのId.
		/// </summary>
		[SerializeField]
		private int coatingId = -1;

		public GameDefine.ThrowingType ThrowingType{ get { return this.type; } }

		public int Power{ get { return this.power; } }

		public int PlayerPower{ get { return this.playerPower; } }

		public DescriptionData.Element DescriptionElement{ get { return new DescriptionData.Element(this.ItemName, this.description, this.Image); } }

		public int RemainingNumber{ get { return this.remainingNumber; } }

		public int CoatingId{ get { return this.coatingId; } }

		public bool IsEmpty{ get { return this.remainingNumber <= 0; } }

		public ThrowingInstanceData(ItemMasterDataBase masterData, int remainingNumber)
		{
			base.InternalCreateFromMasterData(this, masterData);
			var throwingMasterData = masterData as ThrowingMasterData;
			this.type = throwingMasterData.ThrowingType;
			this.power = throwingMasterData.Power;
			this.playerPower = throwingMasterData.PlayerPower;
			this.description = throwingMasterData.Description;
			this.remainingNumber = remainingNumber;
			this.coatingId = -1;
		}

		public ThrowingInstanceData()
		{
			
		}

		public override string ItemName
		{
			get
			{
				if(this.coatingId == -1)
				{
					return ItemManager.Instance.throwingItemReminaingName.Element.Format(this.itemName, this.remainingNumber);
				}
				else
				{
					var coatingItem = ItemManager.Instance.UsableItemList.Database.Find(i => i.Id == coatingId);
					return ItemManager.Instance.coatingThrowingItemReminaingName.Element.Format(coatingItem.ItemName, this.itemName, this.remainingNumber);
				}
			}
		}

		public override int PurchasePrice
		{
			get
			{
				return this.purchasePrice * this.remainingNumber;
			}
		}

		public override int SellingPrice
		{
			get
			{
				return this.sellingPrice * this.remainingNumber;
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.Throwing;
			}
		}

		public void Add(int value)
		{
			this.remainingNumber += value;
			this.remainingNumber = this.remainingNumber > GameDefine.ThrowingItemMax ? GameDefine.ThrowingItemMax : this.remainingNumber;
		}

		public void Throw(CharacterData attacker, IAttack target)
		{
			var damage = Calculator.GetThrowingItemDamage(this);
			target.TakeDamageRaw(attacker, damage, false);

			switch(this.type)
			{
			case GameDefine.ThrowingType.None:
			break;
			case GameDefine.ThrowingType.Coatable:
			break;
			default:
				Debug.AssertFormat(false, "不正な値です. ThrowingType = {0}", this.type);
			break;
			}
			this.remainingNumber--;
		}

		public void Coating(UsableItemInstanceData usableItemInstanceData)
		{
			var coatingItem = new Item(this.MasterData);
			var coatingInstanceData = coatingItem.InstanceData as ThrowingInstanceData;
			var remainingNumber = this.remainingNumber > GameDefine.CreateCoatingThrowingItemNumber ? GameDefine.CreateCoatingThrowingItemNumber : this.remainingNumber;
			coatingInstanceData.coatingId = usableItemInstanceData.Id;
			coatingInstanceData.remainingNumber = remainingNumber;
			this.remainingNumber -= remainingNumber;
			PlayerManager.Instance.AddItem(coatingItem);
		}
	}
}