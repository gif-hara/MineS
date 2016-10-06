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
				equipmentData.LevelUp();
				InformationManager.AddMessage(this.levelUpMessage.Get);
			}
			else
			{
				InformationManager.AddMessage(this.notLevelUpMessage.Get);
			}
		}

		private void OnOpenInventoryUI()
		{
			this.confirmUI.SetActive(false);
		}

		private void OnCloseInventoryUI()
		{
			var inventoryOpenType = PlayerManager.Instance.Data.Inventory.OpenType;
			this.confirmUI.SetActive(inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_Branding
			|| inventoryOpenType == GameDefine.InventoryModeType.BlackSmith_Reinforcement);
		}
	}
}