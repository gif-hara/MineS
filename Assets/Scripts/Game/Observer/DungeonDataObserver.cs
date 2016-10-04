using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DungeonDataObserver : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> receivers;

		public void ModifiedData(DungeonData dungeonData)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedDungeonData>(this.receivers, null, (handler, eventData) => handler.OnModifiedDungeonData(dungeonData));
		}
	}
}