using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ConfirmSelectItemButtonController : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private Text text;

		private Item selectItem;

		private GameDefine.SelectItemDecideType selectItemDecideType;

		public void Initialize(Item selectItem, string message, GameDefine.SelectItemDecideType type)
		{
			this.selectItem = selectItem;
			this.text.text = message;
			this.selectItemDecideType = type;
		}

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			switch(this.selectItemDecideType)
			{
			case GameDefine.SelectItemDecideType.Use:
				this.OnUse();
			break;
			case GameDefine.SelectItemDecideType.Equipment:
				this.OnEquipment();
			break;
			case GameDefine.SelectItemDecideType.Remove:
				this.OnRemove();
			break;
			case GameDefine.SelectItemDecideType.Throw:
				this.OnThrow();
			break;
			case GameDefine.SelectItemDecideType.Put:
				this.OnPut();
			break;
			case GameDefine.SelectItemDecideType.Description:
				this.OnDescription();
			break;
			case GameDefine.SelectItemDecideType.Cancel:
				this.OnCancel();
			break;
			case GameDefine.SelectItemDecideType.Reinforcement:
				this.OnReinforcement();
			break;
			}
		}

#endregion

		private void OnUse()
		{
			var playerManager = PlayerManager.Instance;
			this.selectItem.Use(playerManager.Data);
			playerManager.NotifyCharacterDataObservers();
			playerManager.UpdateInventoryUI();
			playerManager.CloseConfirmSelectItemUI();
		}

		private void OnEquipment()
		{
			this.OnUse();
		}

		private void OnRemove()
		{
			var playerManager = PlayerManager.Instance;
			var inventory = playerManager.Data.Inventory;
			if(inventory.IsFreeSpace)
			{
				inventory.RemoveEquipment(this.selectItem);
				inventory.AddItem(this.selectItem);
				playerManager.NotifyCharacterDataObservers();
				playerManager.UpdateInventoryUI();
				playerManager.CloseConfirmSelectItemUI();
			}
			else
			{
				DescriptionManager.Instance.DeployEmergency("DoNotRemoveEquipment");
			}
		}

		private void OnThrow()
		{
			Debug.Log("「投げる」は未実装だよ！");
		}

		private void OnPut()
		{
			Debug.Log("「置く」は未実装だよ！");
		}

		private void OnDescription()
		{
			DescriptionManager.Instance.Deploy(this.selectItem);
		}

		private void OnCancel()
		{
			PlayerManager.Instance.CloseConfirmSelectItemUI();
		}

		private void OnReinforcement()
		{
			BlackSmithManager.Instance.InvokeReinforcement(this.selectItem);
			PlayerManager.Instance.CloseConfirmSelectItemUI();
		}
	}
}