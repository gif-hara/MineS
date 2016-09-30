using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ItemObserver : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> receivers;

		public void ModifiedData(Item item)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedItem>(this.receivers, null, (handler, eventData) => handler.OnModifiedItem(item));
		}
	}
}