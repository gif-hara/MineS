using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using UnityEngine.Events;

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
		private GameObject bankUI;

		[SerializeField]
		private Text bankMoney;

		[SerializeField]
		private InputField bankInputField;

		[SerializeField]
		private StringAsset.Finder bankFormat;

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

		private const string BankMoneyKeyName = "BankMoney";

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
				var playerManager = PlayerManager.Instance;
				playerManager.RemoveInventoryItemOrEquipment(item);
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
			if(playerManager.AddItem(item))
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
				this.OnWareHouse();
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
			this.OnBankInternal(this.leaveMessage, this.SetLimitBankLeave, money =>
			{
				PlayerManager.Instance.AddMoney(-money, true);
				this.AddMoney(money);
			});
		}

		private void OnBankDraw()
		{
			this.OnBankInternal(this.drawMessage, this.SetLimitBankDraw, money =>
			{
				PlayerManager.Instance.AddMoney(money, true);
				this.AddMoney(-money);
			});
		}

		private void OnBankInternal(StringAsset.Finder invokeMessage, UnityAction<string> limitCall, System.Action<int> moneyAction)
		{
			this.bankUI.SetActive(true);
			this.bankInputField.text = "";
			this.bankInputField.onValueChanged.AddListener(limitCall);
			this.bankMoney.text = this.bankFormat.Format(this.Money);
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(invokeMessage, () =>
			{
				this.bankInputField.onValueChanged.RemoveListener(limitCall);
				this.bankUI.SetActive(false);
				this.OnBank();
				int money;
				if(int.TryParse(this.bankInputField.text, out money))
				{
					moneyAction(money);
				}
			}, true);
			confirmManager.Add(this.cancelMessage, () =>
			{
				this.bankUI.SetActive(false);
				this.OnBank();
			}, true);
		}

		private void OnClosed()
		{
			this.ui.SetActive(false);
		}

		private void AddMoney(int value)
		{
			var money = this.Money;
			money += value;
			money = money > GameDefine.BankMoneyMax ? GameDefine.BankMoneyMax : money;
			HK.Framework.SaveData.SetInt(BankMoneyKeyName, money);
		}

		private int Money
		{
			get
			{
				return HK.Framework.SaveData.GetInt(BankMoneyKeyName);
			}
		}

		private void SetLimitBankLeave(string text)
		{
			var value = int.Parse(text);
			if(value > PlayerManager.Instance.Data.Money)
			{
				this.bankInputField.text = PlayerManager.Instance.Data.Money.ToString();
			}
		}

		private void SetLimitBankDraw(string text)
		{
			var value = int.Parse(text);
			var max = Mathf.Min(this.Money, GameDefine.MoneyMax - PlayerManager.Instance.Data.Money);
			if(value > max)
			{
				this.bankInputField.text = max.ToString();
			}
		}
	}
}