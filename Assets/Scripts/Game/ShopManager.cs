using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ShopManager : SingletonMonoBehaviour<ShopManager>
	{
		[SerializeField]
		private GameObject ui;

		[SerializeField]
		private StringAsset.Finder buyMessage;

		[SerializeField]
		private StringAsset.Finder sellMessage;

		[SerializeField]
		private StringAsset.Finder closedMessage;

		[SerializeField]
		private StringAsset.Finder cancelMessage;

		[SerializeField]
		private StringAsset.Finder welcomeMessage;

		[SerializeField]
		private StringAsset.Finder goodbyeMessage;

		[SerializeField]
		private StringAsset.Finder confirmBuyItemMessage;

		[SerializeField]
		private StringAsset.Finder notBuyMoneyMessage;

		[SerializeField]
		private StringAsset.Finder notBuyNotFreeSpaceMessage;

		[SerializeField]
		private StringAsset.Finder successBuyMessage;

		[SerializeField]
		private StringAsset.Finder confirmSellItemMessage;

		[SerializeField]
		private StringAsset.Finder successSellMessage;

		[SerializeField]
		private List<ItemDataBase> debugItems;

		private Inventory goods;

		void Start()
		{
			PlayerManager.Instance.AddCloseInventoryUIEvent(this.OnCloseInventoryUI);
		}

		public void OpenUI(Inventory goods)
		{
			this.goods = goods;
			this.ui.SetActive(true);
			this.CreateConfirm();
			InformationManager.AddMessage(this.welcomeMessage.Get);
			PlayerManager.Instance.NotifyCharacterDataObservers();
		}

		public void Buy(Inventory addInventory, Item item)
		{
			InformationManager.AddMessage(this.confirmBuyItemMessage.Format(item.InstanceData.ItemName, item.InstanceData.PurchasePrice));
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.buyMessage, () =>
			{
				if(PlayerManager.Instance.Data.Money < item.InstanceData.PurchasePrice)
				{
					InformationManager.AddMessage(this.notBuyMoneyMessage.Get);
					return;
				}
				if(!addInventory.IsFreeSpace)
				{
					InformationManager.AddMessage(this.notBuyNotFreeSpaceMessage.Get);
					return;
				}
				InformationManager.AddMessage(this.successBuyMessage.Get);
				addInventory.AddItem(item);
				this.goods.RemoveItem(item);
				var playerManager = PlayerManager.Instance;
				playerManager.Data.AddMoney(-item.InstanceData.PurchasePrice);
				playerManager.UpdateInventoryUI(this.goods);
				playerManager.NotifyCharacterDataObservers();
			}, true);
			confirmManager.Add(this.cancelMessage, null, true);
		}

		public void Sell(Inventory sellInventory, Item item)
		{
			InformationManager.AddMessage(this.confirmSellItemMessage.Format(item.InstanceData.ItemName, item.InstanceData.SellingPrice));
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.sellMessage, () =>
			{
				InformationManager.AddMessage(this.successSellMessage.Get);
				sellInventory.RemoveItem(item);
				this.goods.AddItemNoLimit(item);
				var playerManager = PlayerManager.Instance;
				playerManager.Data.AddMoney(item.InstanceData.SellingPrice);
				playerManager.NotifyCharacterDataObservers();
				playerManager.UpdateInventoryUI(sellInventory);
			}, true);
			confirmManager.Add(this.cancelMessage, null, true);
		}

		private void OnCloseInventoryUI(Inventory inventory)
		{
			var openType = inventory.OpenType;
			if(openType == GameDefine.InventoryModeType.Shop_Buy
			   || openType == GameDefine.InventoryModeType.Shop_Sell)
			{
				this.CreateConfirm();
			}
		}

		private void CreateConfirm()
		{
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.buyMessage, this.OnBuy, true);
			confirmManager.Add(this.sellMessage, this.OnSell, true);
			confirmManager.Add(this.closedMessage, this.OnClosed, true);
		}

		private void OnBuy()
		{
			PlayerManager.Instance.OpenInventoryUI(GameDefine.InventoryModeType.Shop_Buy, this.goods);
		}

		private void OnSell()
		{
			var playerManager = PlayerManager.Instance;
			playerManager.OpenInventoryUI(GameDefine.InventoryModeType.Shop_Sell, playerManager.Data.Inventory);
		}

		private void OnClosed()
		{
			InformationManager.AddMessage(this.goodbyeMessage.Get);
			this.ui.SetActive(false);
		}
	}
}