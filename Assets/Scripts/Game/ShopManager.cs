using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ShopManager : SingletonMonoBehaviour<ShopManager>
	{
		[SerializeField]
		private Sprite npcImage;

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
		private TalkChunkData firstVisitTalk;

		[SerializeField]
		private TalkChunkData firstVisitTownTalk;

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
			this.OpenNPCUI();
			this.CreateConfirm();
			InformationManager.AddMessage(this.welcomeMessage.Get);
		}

		public void OpenNPCUI()
		{
			NPCManager.Instance.Open(this.npcImage);
		}

		public void InvokeFirstTalk(UnityAction onEndEvent)
		{
			TalkManager.Instance.StartTalk(this.firstVisitTalk, onEndEvent);
		}

		public void InvokeFirstTalkTown(UnityAction onEndEvent)
		{
			TalkManager.Instance.StartTalk(this.firstVisitTownTalk, onEndEvent);
		}

		public void Buy(Item item)
		{
			InformationManager.AddMessage(this.confirmBuyItemMessage.Format(item.InstanceData.ItemName, item.InstanceData.PurchasePrice));
			var playerManager = PlayerManager.Instance;
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.buyMessage, () =>
			{
				if(PlayerManager.Instance.Data.Money < item.InstanceData.PurchasePrice)
				{
					InformationManager.AddMessage(this.notBuyMoneyMessage.Get);
					return;
				}
				if(!playerManager.Data.Inventory.IsFreeSpace)
				{
					InformationManager.AddMessage(this.notBuyNotFreeSpaceMessage.Get);
					return;
				}
				InformationManager.AddMessage(this.successBuyMessage.Get);
				this.goods.RemoveItem(item);
				playerManager.AddItem(item);
				playerManager.AddMoney(-item.InstanceData.PurchasePrice, true);
				playerManager.UpdateInventoryUI();
				playerManager.NotifyCharacterDataObservers();
			}, true);
			confirmManager.Add(this.cancelMessage, null, true);
		}

		public void Sell(Item item)
		{
			InformationManager.AddMessage(this.confirmSellItemMessage.Format(item.InstanceData.ItemName, item.InstanceData.SellingPrice));
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.sellMessage, () =>
			{
				var playerManager = PlayerManager.Instance;
				InformationManager.AddMessage(this.successSellMessage.Get);
				playerManager.RemoveInventoryItemOrEquipment(item);
				this.goods.AddItemNoLimit(item);
				playerManager.AddMoney(item.InstanceData.SellingPrice, true);
				playerManager.NotifyCharacterDataObservers();
				playerManager.UpdateInventoryUI();
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
			NPCManager.Instance.SetActiveUI(false);
		}
	}
}