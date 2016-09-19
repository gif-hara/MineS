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
	public class DescriptionDataObserver : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> receives;

		public void ModifiedData(DescriptionData.Element data)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedDescriptionData>(this.receives, null, (handler, eventData) => handler.OnModifiedDescriptionData(data));
		}

		public void ModifiedData(CharacterData data)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedDescriptionData>(this.receives, null, (handler, eventData) => handler.OnModifiedDescriptionData(data));
		}
	}
}