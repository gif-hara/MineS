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
		private Sprite npcImage;

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

		[SerializeField]
		private TalkChunkData firstVisitTalk;

		[SerializeField]
		private TalkChunkData firstVisitTownTalk;

		public Item SynthesisTargetEquipment{ private set; get; }

		void Start()
		{
			var playerManager = PlayerManager.Instance;
			playerManager.AddCloseInventoryUIEvent(this.OnCloseInventoryUI);
		}

		public void OpenUI()
		{
			InformationManager.AddMessage(this.welcomeMessage.Get);
			this.OpenNPCUI();
			this.CreateConfirm();
		}

		public void OpenNPCUI()
		{
			NPCManager.Instance.SetImage(this.npcImage);
			NPCManager.Instance.SetActiveUI(true);
			PlayerManager.Instance.NotifyCharacterDataObservers();
		}

		public void InvokeFirstTalk(UnityAction onEndEvent)
		{
			TalkManager.Instance.StartTalk(this.firstVisitTalk, onEndEvent);
		}

		public void InvokeFirstTalkTown(UnityAction onEndEvent)
		{
			TalkManager.Instance.StartTalk(this.firstVisitTownTalk, onEndEvent);
		}

		public void InvokeReinforcement(Item item)
		{
			var equipmentData = item.InstanceData as EquipmentInstanceData;
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
				var playerManager = PlayerManager.Instance;
				if(playerManager.Data.Money >= equipmentData.NeedLevelUpMoney)
				{
					playerManager.AddMoney(-equipmentData.NeedLevelUpMoney, false);
					equipmentData.LevelUp();
					InformationManager.AddMessage(this.levelUpMessage.Get);
					playerManager.Serialize();
					playerManager.UpdateInventoryUI(playerManager.Data.Inventory);
					playerManager.NotifyCharacterDataObservers();
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
			var equipmentData = targetEquipment.InstanceData as EquipmentInstanceData;
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
				var playerManager = PlayerManager.Instance;
				var needMoney = Calculator.GetSynthesisNeedMoney(baseEquipment, this.SynthesisTargetEquipment);
				if(playerManager.Data.Money >= needMoney)
				{
					playerManager.AddMoney(-needMoney, false);
					(baseEquipment.InstanceData as EquipmentInstanceData).Synthesis(this.SynthesisTargetEquipment);
					InformationManager.AddMessage(this.successSynthesisMessage.Get);
					playerManager.RemoveInventoryItemOrEquipment(this.SynthesisTargetEquipment);
					playerManager.Data.Inventory.SetSelectItem(null);
					playerManager.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_SynthesisSelectBaseEquipment, playerManager.Data.Inventory);
					playerManager.NotifyCharacterDataObservers();
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
			var playerManager = PlayerManager.Instance;
			var removeItem = playerManager.Data.Inventory.SelectItem;
			var needMoney = Calculator.GetRemoveAbilityNeedMoney(removeItem);
			var equipmentData = removeItem.InstanceData as EquipmentInstanceData;
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
				if(playerManager.Data.Money >= needMoney)
				{
					InformationManager.AddMessage(this.successRemoveAbilityMessage.Get);
					equipmentData.RemoveAbility(index);
					playerManager.AddMoney(-needMoney, false);
					playerManager.Serialize();
					PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.BlackSmith_RemoveAbilitySelectBaseEquipment, playerManager.Data.Inventory);
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
			var equipmentData = item.InstanceData as EquipmentInstanceData;
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
			var equipmentData = item.InstanceData as EquipmentInstanceData;
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
			NPCManager.Instance.SetActiveUI(false);
		}

	}
}