using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DebugAcquireItem : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private Text text;

		private ItemDataBase item;

		public void Initialize(ItemDataBase item)
		{
			this.item = item;
			this.text.text = this.item.ItemName;
		}

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			var playerManager = PlayerManager.Instance;
			if(playerManager.Data.Inventory.IsFreeSpace)
			{
				playerManager.AddItem(new Item(this.item), null);
			}
			else
			{
				Debug.LogWarning("空きがないよ.");
			}
		}

#endregion
	}
}