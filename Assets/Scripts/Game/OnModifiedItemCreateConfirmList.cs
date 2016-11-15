using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedItemCreateConfirmList : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private StringAsset.Finder useFormat;

		[SerializeField]
		private StringAsset.Finder equipmentFormat;

		[SerializeField]
		private StringAsset.Finder removeFormat;

		[SerializeField]
		private StringAsset.Finder throwFormat;

		[SerializeField]
		private StringAsset.Finder putFormat;

		[SerializeField]
		private StringAsset.Finder descriptionFormat;

		[SerializeField]
		private StringAsset.Finder shootFormat;

		[SerializeField]
		private StringAsset.Finder cancelFormat;

		private List<GameObject> createdObjects = new List<GameObject>();

		void OnDisable()
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.createdObjects.Clear();
		}

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
			var openType = PlayerManager.Instance.Data.Inventory.OpenType;
			if(openType == GameDefine.InventoryModeType.Use)
			{
				this.OnUseMode(item);
			}
		}

#endregion

		private void OnUseMode(Item item)
		{
			if(item.InstanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.CreateOnUsableItem(item);
			}
			else if(GameDefine.IsEquipment(item.InstanceData.ItemType))
			{
				if(PlayerManager.Instance.Data.Inventory.Equipment.IsInEquipment(item))
				{
					this.CreateOnEquipmentFromEquipment(item);
				}
				else
				{
					this.CreateOnEquipmentFromInventory(item);
				}
			}
			else if(item.InstanceData.ItemType == GameDefine.ItemType.Arrow)
			{
				this.CreateOnArrow(item);
			}
			else
			{
				Debug.AssertFormat(false, "不正な値です. ItemType = {0}", item.InstanceData.ItemType);
			}
		}

		private void CreateOnUsableItem(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.useFormat.Get, new UnityAction(() => this.OnUse(selectItem)), true);
			ConfirmManager.Instance.Add(this.throwFormat.Get, new UnityAction(() => this.OnThrow(selectItem)), true);
			this.CreateCommonConfirm(selectItem);
		}

		private void CreateOnEquipmentFromInventory(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.equipmentFormat.Get, new UnityAction(() => this.OnEquipment(selectItem)), true);
			this.CreateCommonConfirm(selectItem);
		}

		private void CreateOnEquipmentFromEquipment(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.removeFormat.Get, new UnityAction(() => this.OnRemove(selectItem)), true);
			this.CreateCommonConfirm(selectItem);
		}

		private void CreateOnArrow(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.shootFormat.Get, this.OnCancel, true);
			this.CreateCommonConfirm(selectItem);
		}

		private void CreateCommonConfirm(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.putFormat.Get, new UnityAction(() => this.OnPut(selectItem)), true);
			ConfirmManager.Instance.Add(this.descriptionFormat.Get, new UnityAction(() => this.OnDescription(selectItem)), true);
			ConfirmManager.Instance.Add(this.cancelFormat.Get, this.OnCancel, true);
		}

		private void OnUse(Item item)
		{
			var playerManager = PlayerManager.Instance;
			item.Use(playerManager.Data);
			playerManager.NotifyCharacterDataObservers();
			playerManager.UpdateInventoryUI(playerManager.Data.Inventory);
			playerManager.CloseConfirmSelectItemUI();
		}

		private void OnEquipment(Item item)
		{
			this.OnUse(item);
		}

		private void OnRemove(Item item)
		{
			var playerManager = PlayerManager.Instance;
			var inventory = playerManager.Data.Inventory;
			if(inventory.IsFreeSpace)
			{
				playerManager.RemoveEquipment(item);
				playerManager.NotifyCharacterDataObservers();
				playerManager.UpdateInventoryUI(inventory);
				playerManager.CloseConfirmSelectItemUI();
			}
			else
			{
				DescriptionManager.Instance.DeployEmergency("DoNotRemoveEquipment");
			}
		}

		private void OnThrow(Item item)
		{
			PlayerManager.Instance.CloseInventoryUI();
			PlayerManager.Instance.Data.Inventory.SetSelectItem(item);
			CellManager.Instance.ChangeCellClickMode(GameDefine.CellClickMode.ThrowItem);
		}

		private void OnPut(Item item)
		{
			PlayerManager.Instance.CloseInventoryUI();
			PlayerManager.Instance.Data.Inventory.SetSelectItem(item);
			CellManager.Instance.ChangeCellClickMode(GameDefine.CellClickMode.PutItem);
		}

		private void OnDescription(Item item)
		{
			DescriptionManager.Instance.Deploy(item);
		}

		private void OnCancel()
		{
		}
	}
}