using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class BlackSmithManager : SingletonMonoBehaviour<BlackSmithManager>
	{
		[SerializeField]
		private GameObject ui;

		[SerializeField]
		private StringAsset.Finder welcomeMessage;

		[SerializeField]
		private StringAsset.Finder goodbyeMessage;

		[SerializeField]
		private StringAsset.Finder reinforcementMessage;

		[SerializeField]
		private StringAsset.Finder synthesisMessage;

		[SerializeField]
		private StringAsset.Finder removeAbilityMessage;
		
		[SerializeField]
		private StringAsset.Finder closedMessage;

		[SerializeField]
		private StringAsset.Finder cancelMessage;

		[SerializeField]
		private StringAsset.Finder levelUpMessage;

		[SerializeField]
		private StringAsset.Finder notLevelUpMessage;

		[SerializeField]
		private StringAsset.Finder successSynthesisMessage;

		[SerializeField]
		private StringAsset.Finder notSynthesisMessage;

		[SerializeField]
		private StringAsset.Finder notPossessionEquipmentMessage;

		[SerializeField]
		private StringAsset.Finder startReinforcementMessage;

		[SerializeField]
		private StringAsset.Finder startSynthesisMessage;

		[SerializeField]
		private StringAsset.Finder startRemoveAbilityMessage;

		[SerializeField]
		private StringAsset.Finder confirmReinforceMessage;

		[SerializeField]
		private StringAsset.Finder notLevelUpFromLimitMessage;

		[SerializeField]
		private StringAsset.Finder selectSynthesisTargetEquipmentMessage;

		[SerializeField]
		private StringAsset.Finder notSynthesisBaseEquipmentMessage;

		[SerializeField]
		private StringAsset.Finder confirmSynthesisMessage;

		[SerializeField]
		private StringAsset.Finder canNotSynthesisTargetMessage;

		[SerializeField]
		private StringAsset.Finder notExistBrandingEquipmentMessage;

		[SerializeField]
		private StringAsset.Finder selectRemoveAbilityMessage;

		[SerializeField]
		private StringAsset.Finder confirmRemoveAbilityMessage;

		[SerializeField]
		private StringAsset.Finder successRemoveAbilityMessage;

		[SerializeField]
		private StringAsset.Finder notRemoveAbilityMessage;

		[SerializeField]
		private StringAsset.Finder notRemoveAbilityMessageFromAbilityMatch;

		public Item SynthesisTargetEquipment{ private set; get; }

		void Start()
		{
			var playerManager = PlayerManager.Instance;
			playerManager.AddCloseInventoryUIEvent(this.OnCloseInventoryUI);
		}

		public void OpenUI()
		{
			InformationManager.AddMessage(this.welcomeMessage.Get);
			this.ui.SetActive(true);
			this.CreateConfirm();
			PlayerManager.Instance.NotifyCharacterDataObservers();
		}

		public void InvokeReinforcement(Item item)
		{
			var equipmentData = item.InstanceData as EquipmentData;
			if(equipmentData.CanLevelUp)
			{
				InformationManager.AddMessage(this.confirmReinforceMessage.Format(equipmentData.NeedLevelUpMoney));
			}
			else
			{
				InformationManager.AddMessage(this.notLevelUpFromLimitMessage.Get);
				return;
			}

			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.reinforcementMessage, () =>
			{
				var playerData = PlayerManager.Instance.Data;
				if(playerData.Money >= equipmentData.NeedLevelUpMoney)
				{
					playerData.AddMoney(-equipmentData.NeedLevelUpMoney);
					equipmentData.LevelUp();
					InformationManager.AddMessage(this.levelUpMessage.Get);
					PlayerManager.instance.UpdateInventoryUI(playerData.Inventory);
					PlayerManager.instance.NotifyCharacterDataObservers();
					SEManager.instance.PlaySE(SEManager.instance.blackSmith);
				}
				else
				{
					InformationManager.AddMessage(this.notLevelUpMessage.Get);
				}
			}, true);
			confirmManager.Add(this.cancelMessage, null, true);
		}

		public void InvokeSynthesis(Item targetEquipment)
		{
			var equipmentData = targetEquipment.InstanceData as EquipmentData;
			if(!equipmentData.CanExtraction)
			{
				InformationManager.AddMessage(this.canNotSynthesisTargetMessage.Format(equipmentData.ItemName));
				return;
			}

			var baseEquipment = PlayerManager.Instance.Data.Inventory.SelectItem;
			if(equipmentData.ExistBranding)
			{
				InformationManager.AddMessage(this.confirmSynthesisMessage.Format(baseEquipment.InstanceData.ItemName, targetEquipment.InstanceData.ItemName, Calculator.GetSynthesisNeedMoney(baseEquipment, targetEquipment)));
			}
			else
			{
				InformationManager.AddMessage(this.notExistBrandingEquipmentMessage.Get);
				return;
			}
			this.SynthesisTargetEquipment = targetEquipment;

			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.synthesisMessage, () =>
			{
				var playerData = PlayerManager.Instance.Data;
				var needMoney = Calculator.GetSynthesisNeedMoney(baseEquipment, this.SynthesisTargetEquipment);
				if(playerData.Money >= needMoney)
				{
					playerData.AddMoney(-needMoney);
					(baseEquipment.InstanceData as EquipmentData).Synthesis(this.SynthesisTargetEquipment);
					InformationManager.AddMessage(this.successSynthesisMessage.Get);
					playerData.Inventory.RemoveItemOrEquipment(this.SynthesisTargetEquipment);
					playerData.Inventory.SetSelectItem(null);
					PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_SynthesisSelectBaseEquipment, playerData.Inventory);
					PlayerManager.Instance.NotifyCharacterDataObservers();
					SEManager.instance.PlaySE(SEManager.instance.blackSmith);
				}
				else
				{
					InformationManager.AddMessage(this.notSynthesisMessage.Get);
				}
			}, true);
			confirmManager.Add(this.cancelMessage, null, true);
		}

		public void InvokeRemoveAbility(int index)
		{
			var playerData = PlayerManager.Instance.Data;
			var removeItem = playerData.Inventory.SelectItem;
			var needMoney = Calculator.GetRemoveAbilityNeedMoney(removeItem);
			var equipmentData = removeItem.InstanceData as EquipmentData;
			var removeAbility = equipmentData.Abilities[index];

			if(!equipmentData.CanRemoveAbility(index))
			{
				InformationManager.AddMessage(this.notRemoveAbilityMessageFromAbilityMatch.Get);
				return;
			}

			InformationManager.AddMessage(this.confirmRemoveAbilityMessage.Format(needMoney, removeAbility.Name));
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.removeAbilityMessage, () =>
			{
				if(playerData.Money >= needMoney)
				{
					InformationManager.AddMessage(this.successRemoveAbilityMessage.Get);
					equipmentData.RemoveAbility(index);
					playerData.AddMoney(-needMoney);
					PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_RemoveAbilitySelectBaseEquipment, playerData.Inventory);
					PlayerManager.Instance.NotifyCharacterDataObservers();
					SEManager.instance.PlaySE(SEManager.instance.blackSmith);
				}
				else
				{
					InformationManager.AddMessage(this.notRemoveAbilityMessage.Get);
				}
			}, true);
			confirmManager.Add(this.cancelMessage, null, true);
		}

		public void SetSynthesisBaseEquipment(Item item)
		{
			var equipmentData = item.InstanceData as EquipmentData;
			if(equipmentData.CanSynthesis)
			{
				InformationManager.AddMessage(this.selectSynthesisTargetEquipmentMessage.Get);
			}
			else
			{
				InformationManager.AddMessage(this.notSynthesisBaseEquipmentMessage.Get);
				return;
			}

			var playerManager = PlayerManager.Instance;
			playerManager.Data.Inventory.SetSelectItem(item);
			playerManager.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_SynthesisSelectTargetEquipment, playerManager.Data.Inventory);
		}

		public void SetRemoveAbilityBaseEquipment(Item item)
		{
			var equipmentData = item.InstanceData as EquipmentData;
			if(equipmentData.ExistBranding)
			{
				InformationManager.AddMessage(this.selectRemoveAbilityMessage.Get);
			}
			else
			{
				InformationManager.AddMessage(this.notExistBrandingEquipmentMessage.Get);
				return;
			}

			var playerManager = PlayerManager.Instance;
			playerManager.Data.Inventory.SetSelectItem(item);
			playerManager.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_RemoveAbilitySelectAbility, playerManager.Data.Inventory);
		}

		private void OnCloseInventoryUI(Inventory inventory)
		{
			var inventoryOpenType = inventory.OpenType;
			if(inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_SynthesisSelectBaseEquipment
			   || inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_SynthesisSelectTargetEquipment
			   || inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_Reinforcement
			   || inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_RemoveAbilitySelectAbility
			   || inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_RemoveAbilitySelectBaseEquipment)
			{
				this.CreateConfirm();
			}
		}

		private void CreateConfirm()
		{
			var existEquipment = PlayerManager.Instance.Data.Inventory.IsPossessionEquipment;
			ConfirmManager.Instance.Add(this.reinforcementMessage, new UnityAction(() => this.OnStartJob(this.startReinforcementMessage, GameDefine.InventoryModeType.BlackSmith_Reinforcement)), existEquipment);
			ConfirmManager.Instance.Add(this.synthesisMessage, new UnityAction(() => this.OnStartJob(this.startSynthesisMessage, GameDefine.InventoryModeType.BlackSmith_SynthesisSelectBaseEquipment)), existEquipment);
			ConfirmManager.Instance.Add(this.removeAbilityMessage, new UnityAction(() => this.OnStartJob(this.startRemoveAbilityMessage, GameDefine.InventoryModeType.BlackSmith_RemoveAbilitySelectBaseEquipment)), existEquipment);
			ConfirmManager.Instance.Add(this.closedMessage, this.OnClosed, true);
		}

		private void OnStartJob(StringAsset.Finder message, GameDefine.InventoryModeType inventoryMode)
		{
			var playerManager = PlayerManager.Instance;
			if(!playerManager.Data.Inventory.IsPossessionEquipment)
			{
				InformationManager.AddMessage(this.notPossessionEquipmentMessage.Get);
				return;
			}

			InformationManager.AddMessage(message.Get);
			playerManager.OpenInventoryUI(inventoryMode, playerManager.Data.Inventory);
		}

		private void OnClosed()
		{
			InformationManager.AddMessage(this.goodbyeMessage.Get);
			this.ui.SetActive(false);
		}

	}
}