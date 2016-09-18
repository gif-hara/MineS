using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayerDataObserver : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> receivers;

		public void ModifiedData(PlayerData data)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedPlayerData>(this.receivers, null, (handler, eventData) => handler.OnModifiedPlayerData(data));
		}
	}
}