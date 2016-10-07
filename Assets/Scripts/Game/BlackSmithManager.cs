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
		private StringAsset.Finder reinforcementMessage;

		[SerializeField]
		private StringAsset.Finder synthesisMessage;
		
		[SerializeField]
		private StringAsset.Finder closedMessage;

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

		public Item SynthesisTargetEquipment{ private set; get; }

		void Start()
		{
			var playerManager = PlayerManager.Instance;
			playerManager.AddCloseInventoryUIEvent(this.OnCloseInventoryUI);
			this.OpenUI();
		}

		public void OpenUI()
		{
			this.ui.SetActive(true);
			this.CreateConfirm();
			PlayerManager.Instance.NotifyCharacterDataObservers();
		}

		public void InvokeReinforcement(Item item)
		{
			var equipmentData = item.InstanceData as EquipmentData;

			var playerData = PlayerManager.Instance.Data;
			if(playerData.Money >= equipmentData.NeedLevelUpMoney)
			{
				playerData.AddMoney(-equipmentData.NeedLevelUpMoney);
				equipmentData.LevelUp();
				InformationManager.AddMessage(this.levelUpMessage.Get);
				PlayerManager.instance.UpdateInventoryUI(playerData.Inventory);
				PlayerManager.instance.NotifyCharacterDataObservers();
			}
			else
			{
				InformationManager.AddMessage(this.notLevelUpMessage.Get);
			}
		}

		public void InvokeSynthesis()
		{
			
			var playerData = PlayerManager.Instance.Data;
			var baseEquipment = playerData.Inventory.SelectItem;
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
			}
			else
			{
				InformationManager.AddMessage(this.notSynthesisMessage.Get);
			}
		}

		public void SetSynthesisTargetEquipment(Item item)
		{
			this.SynthesisTargetEquipment = item;
		}

		private void OnCloseInventoryUI()
		{
			var inventoryOpenType = PlayerManager.Instance.Data.Inventory.OpenType;
			if(inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_SynthesisSelectBaseEquipment
			   || inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_SynthesisSelectTargetEquipment
			   || inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_Reinforcement)
			{
				this.CreateConfirm();
			}
		}

		private void CreateConfirm()
		{
			ConfirmManager.Instance.Add(this.reinforcementMessage, new UnityAction(() => this.OnStartJob(this.startReinforcementMessage, GameDefine.InventoryModeType.BlackSmith_Reinforcement)), true);
			ConfirmManager.Instance.Add(this.synthesisMessage, new UnityAction(() => this.OnStartJob(this.startSynthesisMessage, GameDefine.InventoryModeType.BlackSmith_SynthesisSelectBaseEquipment)), true);
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
			this.ui.SetActive(false);
		}

	}
}