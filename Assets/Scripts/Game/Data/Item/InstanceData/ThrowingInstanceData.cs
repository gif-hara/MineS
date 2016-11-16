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
			this.remainingNumber--;

			switch(this.type)
			{
			case GameDefine.ThrowingType.None:
				{
					this.TakeDamage(target, damage);
					Object.Instantiate(EffectManager.Instance.prefabThrowing0.Element, EnemyManager.Instance.InEnemyCells[target as EnemyData].Controller.transform, false);
				}
			break;
			case GameDefine.ThrowingType.Coatable:
				{
					this.TakeDamage(target, damage);
					Object.Instantiate(EffectManager.Instance.prefabThrowing0.Element, EnemyManager.Instance.InEnemyCells[target as EnemyData].Controller.transform, false);
					if(this.coatingId != -1 && !target.IsDead)
					{
						(ItemManager.Instance.UsableItemList.Database.Find(i => i.Id == this.coatingId) as UsableItemMasterData).OnUse(target, 0.5f);
					}
				}
			break;
			case GameDefine.ThrowingType.Diffusion:
				{
					var cellData = EnemyManager.Instance.InEnemyCells[target as EnemyData];
					CellManager.Instance.GetAdjacentCellDataSlanting(cellData.Position, 2).ForEach(c =>
					{
						EnemyData enemy;
						if(EnemyManager.Instance.Enemies.TryGetValue(c, out enemy) && c.IsIdentification)
						{
							this.TakeDamage(enemy, damage);
						}
						Object.Instantiate(EffectManager.Instance.prefabThrowing0.Element, c.Controller.transform, false);
					});
				}
			break;
			case GameDefine.ThrowingType.Bounce:
				{
					this.TakeDamage(target, damage);
					Object.Instantiate(EffectManager.Instance.prefabThrowing0.Element, EnemyManager.Instance.InEnemyCells[target as EnemyData].Controller.transform, false);
					var otherEnemy = EnemyManager.Instance.VisibleEnemies.Where(e => e != (target as EnemyData)).ToList();
					if(otherEnemy.Count > 0)
					{
						var otherTarget = otherEnemy[Random.Range(0, otherEnemy.Count)];
						this.TakeDamage(otherTarget, damage);
						Object.Instantiate(EffectManager.Instance.prefabThrowing0.Element, EnemyManager.Instance.InEnemyCells[otherTarget].Controller.transform, false);
					}
				}
			break;
			case GameDefine.ThrowingType.Cross:
				{
					var cellData = EnemyManager.Instance.InEnemyCells[target as EnemyData];
					CellManager.Instance.GetCrossCellDataAll(cellData.Position).ForEach(c =>
					{
						EnemyData enemy;
						if(EnemyManager.Instance.Enemies.TryGetValue(c, out enemy) && c.IsIdentification)
						{
							this.TakeDamage(enemy, damage);
						}
						Object.Instantiate(EffectManager.Instance.prefabThrowing0.Element, c.Controller.transform, false);
					});
				}
			break;
			default:
				Debug.AssertFormat(false, "不正な値です. ThrowingType = {0}", this.type);
			break;
			}
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

		private void TakeDamage(IAttack target, int damage)
		{
			target.TakeDamageRaw(null, damage, false);
			if(target.IsDead)
			{
				PlayerManager.Instance.Data.Defeat(target);
			}
		}
	}
}