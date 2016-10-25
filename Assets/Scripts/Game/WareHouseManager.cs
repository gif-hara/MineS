using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class WareHouseManager : SingletonMonoBehaviour<WareHouseManager>
	{
		[SerializeField]
		private GameObject ui;

		[SerializeField]
		private StringAsset.Finder wareHouseMessage;

		[SerializeField]
		private StringAsset.Finder bankMessage;

		[SerializeField]
		private StringAsset.Finder closedMessage;

		[SerializeField]
		private StringAsset.Finder leaveMessage;

		[SerializeField]
		private StringAsset.Finder drawMessage;

		[SerializeField]
		private StringAsset.Finder cancelMessage;

		void Start()
		{
			PlayerManager.Instance.AddCloseInventoryUIEvent(this.OnCloseInventoryUI);
		}

		public void OpenUI()
		{
			this.ui.SetActive(true);
			PlayerManager.Instance.NotifyCharacterDataObservers();
			this.CreateConfirm();
		}

		public void LeaveItem(Item item)
		{
			if(MineS.SaveData.WareHouse.Add(item))
			{
				var playerInventory = PlayerManager.Instance.Data.Inventory;
				playerInventory.RemoveItemOrEquipment(item);
				PlayerManager.Instance.UpdateInventoryUI();
				HK.Framework.SaveData.Save();
			}
			else
			{
				Debug.LogWarning("預けられなかった！");
			}
		}

		public void DrawItem(Item item)
		{
			var playerManager = PlayerManager.Instance;
			if(playerManager.Data.Inventory.AddItem(item))
			{
				MineS.SaveData.WareHouse.Remove(item);
				playerManager.UpdateInventoryUI();
				HK.Framework.SaveData.Save();
			}
			else
			{
				Debug.LogWarning("取り出せなかった！");
			}
		}

		private void OnCloseInventoryUI(Inventory inventory)
		{
			var openType = inventory.OpenType;
			if(openType == GameDefine.InventoryModeType.WareHouse_Leave
			   || openType == GameDefine.InventoryModeType.WareHouse_Draw)
			{
				this.CreateConfirm();
			}
		}

		private void CreateConfirm()
		{
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.wareHouseMessage, this.OnWareHouse, true);
			confirmManager.Add(this.bankMessage, this.OnBank, true);
			confirmManager.Add(this.closedMessage, this.OnClosed, true);
		}

		private void OnWareHouse()
		{
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.leaveMessage, this.OnWareHouseLeave, true);
			confirmManager.Add(this.drawMessage, this.OnWareHouseDraw, true);
			confirmManager.Add(this.cancelMessage, this.CreateConfirm, true);
		}

		private void OnWareHouseLeave()
		{
			PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.WareHouse_Leave, PlayerManager.Instance.Data.Inventory);
		}

		private void OnWareHouseDraw()
		{
			PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.WareHouse_Draw, MineS.SaveData.WareHouse.Inventory);
		}

		private void OnBank()
		{
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.leaveMessage, this.OnBankLeave, true);
			confirmManager.Add(this.drawMessage, this.OnBankDraw, true);
			confirmManager.Add(this.cancelMessage, this.CreateConfirm, true);
		}

		private void OnBankLeave()
		{

		}

		private void OnBankDraw()
		{

		}

		private void OnClosed()
		{
			this.ui.SetActive(false);
		}
	}
}