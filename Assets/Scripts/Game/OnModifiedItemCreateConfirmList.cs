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
		private StringAsset.Finder cancelFormat;

		[SerializeField]
		private StringAsset.Finder reinforcementFormat;

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
			else if(openType == GameDefine.InventoryModeType.BlackSmith_Reinforcement)
			{
				this.OnBlackSmithReinforcement(item);
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
			else
			{
				Debug.AssertFormat(false, "不正な値です. ItemType = {0}", item.InstanceData.ItemType);
			}
		}

		private void OnBlackSmithReinforcement(Item item)
		{
			ConfirmManager.Instance.Add(this.reinforcementFormat.Get, new UnityAction(() => this.OnReinforcement(item)), true);
			ConfirmManager.Instance.Add(this.cancelFormat.Get, this.OnCancel, true);
		}

		private void CreateOnUsableItem(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.useFormat.Get, new UnityAction(() => this.OnUse(selectItem)), true);
			ConfirmManager.Instance.Add(this.throwFormat.Get, new UnityAction(() => this.OnThrow(selectItem)), true);
			ConfirmManager.Instance.Add(this.putFormat.Get, new UnityAction(() => this.OnPut(selectItem)), true);
			ConfirmManager.Instance.Add(this.descriptionFormat.Get, new UnityAction(() => this.OnDescription(selectItem)), true);
			ConfirmManager.Instance.Add(this.cancelFormat.Get, this.OnCancel, true);
		}

		private void CreateOnEquipmentFromInventory(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.equipmentFormat.Get, new UnityAction(() => this.OnEquipment(selectItem)), true);
			ConfirmManager.Instance.Add(this.putFormat.Get, new UnityAction(() => this.OnPut(selectItem)), true);
			ConfirmManager.Instance.Add(this.descriptionFormat.Get, new UnityAction(() => this.OnDescription(selectItem)), true);
			ConfirmManager.Instance.Add(this.cancelFormat.Get, this.OnCancel, true);
		}

		private void CreateOnEquipmentFromEquipment(Item selectItem)
		{
			ConfirmManager.Instance.Add(this.removeFormat.Get, new UnityAction(() => this.OnRemove(selectItem)), true);
			ConfirmManager.Instance.Add(this.putFormat.Get, new UnityAction(() => this.OnPut(selectItem)), true);
			ConfirmManager.Instance.Add(this.descriptionFormat.Get, new UnityAction(() => this.OnDescription(selectItem)), true);
			ConfirmManager.Instance.Add(this.cancelFormat.Get, this.OnCancel, true);
		}

		private void OnUse(Item item)
		{
			var playerManager = PlayerManager.Instance;
			item.Use(playerManager.Data);
			playerManager.NotifyCharacterDataObservers();
			playerManager.UpdateInventoryUI();
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
				inventory.RemoveEquipment(item);
				inventory.AddItem(item);
				playerManager.NotifyCharacterDataObservers();
				playerManager.UpdateInventoryUI();
				playerManager.CloseConfirmSelectItemUI();
			}
			else
			{
				DescriptionManager.Instance.DeployEmergency("DoNotRemoveEquipment");
			}
		}

		private void OnThrow(Item item)
		{
			Debug.Log("「投げる」は未実装だよ！");
		}

		private void OnPut(Item item)
		{
			Debug.Log("「置く」は未実装だよ！");
		}

		private void OnDescription(Item item)
		{
			DescriptionManager.Instance.Deploy(item);
		}

		private void OnCancel()
		{
		}

		private void OnReinforcement(Item item)
		{
			BlackSmithManager.Instance.Reinforcement(item);
			PlayerManager.Instance.CloseConfirmSelectItemUI();
		}

	}
}