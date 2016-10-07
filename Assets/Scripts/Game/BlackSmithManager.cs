using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class BlackSmithManager : SingletonMonoBehaviour<BlackSmithManager>
	{
		[SerializeField]
		private GameObject confirmUI;

		[SerializeField]
		private StringAsset.Finder levelUpMessage;

		[SerializeField]
		private StringAsset.Finder notLevelUpMessage;

		[SerializeField]
		private StringAsset.Finder synthesisMessage;

		[SerializeField]
		private StringAsset.Finder notSynthesisMessage;

		public Item BrandingTargetEquipment{ private set; get; }

		void Start()
		{
			var playerManager = PlayerManager.Instance;
			playerManager.AddOpenInventoryUIEvent(this.OnOpenInventoryUI);
			playerManager.AddCloseInventoryUIEvent(this.OnCloseInventoryUI);
		}

		public void Reinforcement(Item item)
		{
			var equipmentData = item.InstanceData as EquipmentData;

			var playerData = PlayerManager.Instance.Data;
			if(playerData.Money > equipmentData.NeedLevelUpMoney)
			{
				playerData.AddMoney(-equipmentData.NeedLevelUpMoney);
				equipmentData.LevelUp();
				InformationManager.AddMessage(this.levelUpMessage.Get);
				PlayerManager.instance.UpdateInventoryUI();
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
			var needMoney = Calculator.GetSynthesisNeedMoney(baseEquipment, this.BrandingTargetEquipment);
			if(playerData.Money > needMoney)
			{
				playerData.AddMoney(-needMoney);
				(baseEquipment.InstanceData as EquipmentData).Synthesis(this.BrandingTargetEquipment);
				InformationManager.AddMessage(this.synthesisMessage.Get);
				playerData.Inventory.RemoveItemOrEquipment(this.BrandingTargetEquipment);
				playerData.Inventory.SetSelectItem(null);
				PlayerManager.Instance.UpdateInventoryUI();
				PlayerManager.Instance.NotifyCharacterDataObservers();
			}
			else
			{
				InformationManager.AddMessage(this.notSynthesisMessage.Get);
			}
		}

		public void SetBrandingTargetEquipment(Item item)
		{
			this.BrandingTargetEquipment = item;
		}

		private void OnOpenInventoryUI()
		{
			this.confirmUI.SetActive(false);
		}

		private void OnCloseInventoryUI()
		{
			var inventoryOpenType = PlayerManager.Instance.Data.Inventory.OpenType;
			this.confirmUI.SetActive(
				inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_BrandingSelectBaseEquipment
				|| inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_BrandingSelectTargetEquipment
				|| inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_BrandingSelectAbility
				|| inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_Reinforcement);
		}
	}
}