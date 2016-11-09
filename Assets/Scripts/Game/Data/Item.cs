using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class Item
	{
		public class ItemEvent : UnityEvent<Item>
		{
		}

		[SerializeField]
		private ItemInstanceDataBase instanceData;

		public ItemInstanceDataBase InstanceData{ get { return this.instanceData; } }

		public bool IsValid{ get { return this.instanceData != null; } }

		private static ItemEvent onUseItemEvent = new ItemEvent();

		public Item(ItemDataBase masterData)
		{
			switch(masterData.ItemType)
			{
			case GameDefine.ItemType.UsableItem:
				this.instanceData = new UsableItemInstanceData(masterData);
			break;
			case GameDefine.ItemType.Accessory:
			case GameDefine.ItemType.Shield:
			case GameDefine.ItemType.Weapon:
				this.instanceData = new EquipmentInstanceData(masterData);
			break;
			default:
				Debug.AssertFormat(false, "不正な値です. masterData.ItemType = {0}", masterData.ItemType);
			break;
			}
		}

		public Item()
		{
		}

		public static void AddOnUseItemEvent(UnityAction<Item> call)
		{
			onUseItemEvent.AddListener(call);
		}

		public void Use(IAttack user)
		{
			if(GameDefine.IsEquipment(this.instanceData.ItemType))
			{
				var changedEquipment = PlayerManager.Instance.Data.Inventory.ChangeEquipment(this);
				PlayerManager.Instance.ChangeItem(this, changedEquipment);
			}
			else if(this.instanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.UseUsableItem(user, PlayerManager.Instance.Data.Inventory);
			}
			else
			{
				Debug.LogWarning("未実装のアイテムです ItemType = " + this.instanceData.ItemType);
			}
			onUseItemEvent.Invoke(this);
		}

		public void Serialize(string key)
		{
			switch(this.instanceData.ItemType)
			{
			case GameDefine.ItemType.UsableItem:
				HK.Framework.SaveData.SetClass<UsableItemInstanceData>(key, this.instanceData as UsableItemInstanceData);
			break;
			case GameDefine.ItemType.Accessory:
			case GameDefine.ItemType.Shield:
			case GameDefine.ItemType.Weapon:
				HK.Framework.SaveData.SetClass<EquipmentInstanceData>(key, this.instanceData as EquipmentInstanceData);
			break;
			default:
				Debug.AssertFormat(false, "不正な値です. itemType = {0}", this.instanceData.ItemType);
			break;
			}
			HK.Framework.SaveData.SetInt(string.Format("{0}_Type", key), (int)this.instanceData.ItemType);
		}

		public static Item Deserialize(string key)
		{
			var typeKey = TypeKey(key);
			if(!HK.Framework.SaveData.ContainsKey(typeKey))
			{
				return null;
			}

			var result = new Item();
			var type = (GameDefine.ItemType)HK.Framework.SaveData.GetInt(typeKey);
			switch(type)
			{
			case GameDefine.ItemType.UsableItem:
				result.instanceData = HK.Framework.SaveData.GetClass<UsableItemInstanceData>(key, null);
			break;
			case GameDefine.ItemType.Accessory:
			case GameDefine.ItemType.Shield:
			case GameDefine.ItemType.Weapon:
				result.instanceData = HK.Framework.SaveData.GetClass<EquipmentInstanceData>(key, null);
				(result.instanceData as EquipmentInstanceData).InitializeAbilities();
			break;
			default:
				Debug.AssertFormat(false, "不正な値です. itemType = {0}", type);
			break;
			}

			return result;
		}

		private static string TypeKey(string key)
		{
			return string.Format("{0}_Type", key);
		}

		private void UseUsableItem(IAttack user, Inventory inventory)
		{
			SEManager.Instance.PlaySE((this.instanceData.MasterData as UsableItemData).UseSound);
			var itemName = this.instanceData.ItemName;
			if(ItemManager.Instance.Identified(this))
			{
				InformationManager.IdentifiedItem(itemName, this.instanceData.ItemNameRaw);
			}

			var usableItem = this.instanceData as UsableItemInstanceData;
			switch(usableItem.UsableItemType)
			{
			case GameDefine.UsableItemType.RecoveryHitPointLimit:
				{
					var value = Calculator.GetUsableItemRecoveryValue(usableItem.RandomPower, user);
					user.RecoveryHitPoint(value, true);
					inventory.RemoveItem(this);
					InformationManager.OnUseRecoveryHitPointItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.RecoveryArmor:
				{
					var value = Calculator.GetUsableItemRecoveryValue(usableItem.RandomPower, user);
					user.RecoveryArmor(value, true);
					inventory.RemoveItem(this);
					InformationManager.OnUseRecoveryArmorItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.AddAbnormalStatus:
				{
					var type = (GameDefine.AbnormalStatusType)usableItem.Power0;
					var addAbnormalResultType = user.AddAbnormalStatus(AbnormalStatusFactory.Create(type, user, usableItem.Power1, 0));
					inventory.RemoveItem(this);
					InformationManager.OnUseAddAbnormalStatusItem(user, type, addAbnormalResultType);
				}
			break;
			case GameDefine.UsableItemType.RemoveAbnormalStatus:
				{
					var type = (GameDefine.AbnormalStatusType)usableItem.Power0;
					user.RemoveAbnormalStatus(type);
					inventory.RemoveItem(this);
					InformationManager.OnUseRemoveAbnormalStatusItem(user, type);
				}
			break;
			case GameDefine.UsableItemType.Damage:
				{
					var damage = usableItem.RandomPower;
					user.TakeDamageRaw(null, damage, false);
					InformationManager.OnUseDamageItem(user, damage);
					if(user.CharacterType == GameDefine.CharacterType.Enemy && user.IsDead)
					{
						PlayerManager.Instance.Data.Defeat(user);
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.NailDown:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						CellManager.Instance.OnUseXrayAll();
						InformationManager.OnUseNailDown();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.CallingOff:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						CellManager.Instance.RemoveTrap();
						InformationManager.OnUseCallingOff();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Drain:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						InformationManager.OnHadNoEffect();
						InformationManager.WillThrowEnemy();
					}
					else
					{
						var playerData = PlayerManager.Instance.Data;
						var damage = user.HitPoint / 2;
						user.TakeDamageRaw(playerData, damage, true);
						playerData.RecoveryHitPoint(damage, true);
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Brake:
				{
					user.TakeDamageArmorOnly(user.Armor, true);
				}
			break;
			case GameDefine.UsableItemType.UndineDrop:
				{
					var value = usableItem.Power0;
					user.AddBaseStrength(usableItem.Power0);
					InformationManager.AddBaseStrength(user, value);
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.UndineTear:
				{
					var value = usableItem.Power0;
					user.AddHitPointMax(usableItem.Power0);
					InformationManager.AddHitPointMax(user, value);
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.UndineBlood:
				{
					var value = usableItem.Power0;
					user.ForceLevelUp(value);
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Alchemy:
				{
					if(user.CharacterType == GameDefine.CharacterType.Enemy)
					{
						user.ForceDead();
						var cellData = EnemyManager.Instance.InEnemyCells[user as EnemyData];
						var item = DungeonManager.Instance.CreateItem();
						cellData.BindCellClickAction(new AcquireItemAction(item));
						cellData.BindDeployDescription(new DeployDescriptionOnItem(item));
						InformationManager.OnUseAlchemy(user);
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Actinidia:
				{
					var blankCell = CellManager.Instance.RandomBlankCell(true);
					if(blankCell == null)
					{
						InformationManager.OnHadNoEffect();
						return;
					}

					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						var enemy = EnemyManager.Instance.Create(blankCell);
						enemy.OnVisible(blankCell);
						InformationManager.OnUseActinidiaByPlayer(enemy);
						PlayerManager.Instance.CloseInventoryUI();
					}
					else if(user.CharacterType == GameDefine.CharacterType.Enemy)
					{
						(user as EnemyData).OnDivision(blankCell);
						InformationManager.OnUseActinidiaByEnemy(user);
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.TurnBack:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player && DungeonManager.Instance.CanTurnBack(-1))
					{
						DungeonManager.Instance.NextFloorEvent(-1, false);
						PlayerManager.Instance.CloseInventoryUI();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Proceed:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						DungeonManager.Instance.NextFloorEvent(1, false);
						PlayerManager.Instance.CloseInventoryUI();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.OndineDrop:
				{
					var value = usableItem.Power0;
					user.AddBaseStrength(usableItem.Power0);
					InformationManager.SubBaseStrength(user, Mathf.Abs(value));
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.OndineTear:
				{
					var value = usableItem.Power0;
					user.AddHitPointMax(usableItem.Power0);
					InformationManager.SubHitPointMax(user, Mathf.Abs(value));
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.OndineBlood:
				{
					var value = usableItem.Power0;
					user.ForceLevelDown(value);
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.ReturnTown:
				{
					user.ReturnTown();
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Water:
				{
					InformationManager.OnHadNoEffect();
					inventory.RemoveItem(this);
				}
			break;
			default:
				Debug.LogWarning("未実装の使用可能アイテムです UsableItemType= " + usableItem.UsableItemType);
			break;
			}

			if(MineS.SaveData.Option.AutoSort)
			{
				inventory.Sort();
			}
			PlayerManager.Instance.Serialize();
		}

	}
}