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

		public Item(ItemMasterDataBase masterData)
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
			case GameDefine.ItemType.Throwing:
				this.instanceData = new ThrowingInstanceData(masterData, Random.Range(3, 10));
			break;
			case GameDefine.ItemType.MagicStone:
				this.instanceData = new MagicStoneInstanceData(masterData, Random.Range(3, 10));
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
			var inventory = PlayerManager.Instance.Data.Inventory;
			if(GameDefine.IsEquipment(this.instanceData.ItemType))
			{
				var changedEquipment = inventory.ChangeEquipment(this);
				PlayerManager.Instance.ChangeItem(this, changedEquipment);
			}
			else if(this.instanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.UseUsableItem(user, inventory);
			}
			else if(this.instanceData.ItemType == GameDefine.ItemType.Throwing)
			{
				this.UseThrowing(user, inventory);
			}
			else if(this.instanceData.ItemType == GameDefine.ItemType.MagicStone)
			{
				this.UseMagicStone(user, inventory);
			}
			else
			{
				Debug.LogWarning("未実装のアイテムです ItemType = " + this.instanceData.ItemType);
			}

			if(MineS.SaveData.Option.AutoSort)
			{
				inventory.Sort();
			}
			PlayerManager.Instance.Serialize();

			onUseItemEvent.Invoke(this);
		}

		/// <summary>
		/// アイテムを識別する
		/// </summary>
		public void Identification(IdentifiedItemManager identifiedManager)
		{
			var itemName = this.instanceData.ItemName;
			if(identifiedManager.Identified(this))
			{
				InformationManager.IdentifiedItem(itemName, this.instanceData.ItemName);
			}
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
			case GameDefine.ItemType.Throwing:
				HK.Framework.SaveData.SetClass<ThrowingInstanceData>(key, this.instanceData as ThrowingInstanceData);
			break;
			case GameDefine.ItemType.MagicStone:
				HK.Framework.SaveData.SetClass<MagicStoneInstanceData>(key, this.instanceData as MagicStoneInstanceData);
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
			case GameDefine.ItemType.Throwing:
				result.instanceData = HK.Framework.SaveData.GetClass<ThrowingInstanceData>(key, null);
			break;
			case GameDefine.ItemType.MagicStone:
				result.instanceData = HK.Framework.SaveData.GetClass<MagicStoneInstanceData>(key, null);
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
			SEManager.Instance.PlaySE((this.instanceData.MasterData as UsableItemMasterData).UseSound);
			inventory.RemoveItem(this);

			if(user.CharacterType == GameDefine.CharacterType.Enemy && user.FindAbnormalStatus(GameDefine.AbnormalStatusType.TrapMaster))
			{
                SEManager.Instance.PlaySE(SEManager.Instance.avoidPlayer);
                InformationManager.InvalidUseItemOnTrapMaster();
                return;
            }

            this.Identification(ItemManager.Instance.UsableItemIdentified);

            (this.instanceData.MasterData as UsableItemMasterData).OnUse(user, 1.0f);
		}

		private void UseThrowing(IAttack user, Inventory inventory)
		{
			var throwingInstanceData = this.instanceData as ThrowingInstanceData;
			throwingInstanceData.Throw(PlayerManager.Instance.Data, user);
			if(throwingInstanceData.IsEmpty)
			{
				inventory.RemoveItem(this);
			}
		}

		private void UseMagicStone(IAttack user, Inventory inventory)
		{
			SEManager.Instance.PlaySE(SEManager.Instance.useMagicStone0);
			Object.Instantiate(EffectManager.Instance.prefabUseMagicStone0.Element, EnemyManager.Instance.InEnemyCells[user as EnemyData].Controller.transform, false);

            var magicStoneInstanceData = this.instanceData as MagicStoneInstanceData;
			magicStoneInstanceData.Use(this, PlayerManager.Instance.Data, user);
			if(magicStoneInstanceData.IsEmpty)
			{
				inventory.RemoveItem(this);
			}
		}
	}
}