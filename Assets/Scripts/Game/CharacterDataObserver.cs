using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CharacterDataObserver : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> receivers;

		public void ModifiedData(CharacterData data)
		{
			ExecuteEventsExtensions.Execute<IReceiveModifiedCharacterData>(this.receivers, null, (handler, eventData) => handler.OnModifiedCharacterData(data));
		}
	}
}