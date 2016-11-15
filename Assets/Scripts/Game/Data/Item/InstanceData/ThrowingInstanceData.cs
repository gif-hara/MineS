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
		private int coatingId;

		public GameDefine.ThrowingType ThrowingType{ get { return this.type; } }

		public int Power{ get { return this.power; } }

		public int PlayerPower{ get { return this.playerPower; } }

		public DescriptionData.Element DescriptionElement{ get { return new DescriptionData.Element(this.ItemName, this.description, this.Image); } }

		public int RemainingNumber{ get { return this.remainingNumber; } }

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
		}

		public ThrowingInstanceData()
		{
			
		}

		public override string ItemName
		{
			get
			{
				return ItemManager.Instance.throwingItemReminaingName.Element.Format(this.itemName, this.remainingNumber);
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
			switch(this.type)
			{
			case GameDefine.ThrowingType.None:
				target.TakeDamageRaw(attacker, damage, false);
			break;
			}
			this.remainingNumber--;
		}
	}
}