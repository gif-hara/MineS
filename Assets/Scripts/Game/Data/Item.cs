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
	public class Item
	{
		public ItemDataBase InstanceData{ private set; get; }

		public Item(ItemDataBase masterData)
		{
			this.InstanceData = masterData.Clone;
		}

		public void Use(IAttack user)
		{
			if(GameDefine.IsEquipment(this.InstanceData.ItemType))
			{
				var changedEquipment = PlayerManager.Instance.Data.Inventory.ChangeEquipment(this);
				PlayerManager.Instance.ChangeItem(this, changedEquipment);
			}
			else if(this.InstanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.UseUsableItem(user);
			}
			else
			{
				Debug.LogWarning("未実装のアイテムです ItemType = " + this.InstanceData.ItemType);
			}
		}

		private void UseUsableItem(IAttack user)
		{
			var playerManager = PlayerManager.Instance;
			var usableItem = this.InstanceData as UsableItemData;
			switch(usableItem.UsableItemType)
			{
			case GameDefine.UsableItemType.RecoveryHitPointLimit:
				{
					var value = Calculator.GetUsableItemRecoveryValue(usableItem.RandomPower, user);
					playerManager.RecoveryHitPoint(value, true);
					playerManager.RemoveInventoryItem(this);
					InformationManager.OnUseRecoveryHitPointItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.RecoveryArmor:
				{
					var value = Calculator.GetUsableItemRecoveryValue(usableItem.RandomPower, user);
					playerManager.RecoveryArmor(value);
					playerManager.RemoveInventoryItem(this);
					InformationManager.OnUseRecoveryArmorItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.AddAbnormalStatus:
				{
					var type = (GameDefine.AbnormalStatusType)usableItem.Power0;
					var addAbnormalResultType = playerManager.AddAbnormalStatus(type, usableItem.Power1, 0);
					playerManager.RemoveInventoryItem(this);
					InformationManager.OnUseAddAbnormalStatusItem(user, type, addAbnormalResultType);
				}
			break;
			case GameDefine.UsableItemType.RemoveAbnormalStatus:
				{
					var type = (GameDefine.AbnormalStatusType)usableItem.Power0;
					playerManager.RemoveAbnormalStatus(type);
					playerManager.RemoveInventoryItem(this);
					InformationManager.OnUseRemoveAbnormalStatusItem(user, type);
				}
			break;
			default:
				Debug.LogWarning("未実装の使用可能アイテムです UsableItemType= " + usableItem.UsableItemType);
			break;
			}

		}

	}
}