using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InventoryObserver : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> receivers;

		public void ModifiedData(Inventory data)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedInventory>(this.receivers, null, (handler, eventData) => handler.OnModifiedInventory(data));
		}
	}
}