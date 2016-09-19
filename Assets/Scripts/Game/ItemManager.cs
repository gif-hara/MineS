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
	public class ItemManager : SingletonMonoBehaviour<ItemManager>
	{
		[SerializeField]
		private List<InventoryObserver> observers;

		public void OpenInventory()
		{
			this.observers.ForEach(o => o.ModifiedData(PlayerManager.Instance.Data.Inventory));
		}
	}
}