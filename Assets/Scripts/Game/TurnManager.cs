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
	public class TurnManager : SingletonMonoBehaviour<TurnManager>
	{
		public class ProgressEvent : UnityEvent<GameDefine.TurnProgressType, int>
		{
			
		}

		private ProgressEvent progressEvent = new ProgressEvent();

		private int count = 0;

		public void Progress(GameDefine.TurnProgressType type)
		{
			this.count++;
			this.progressEvent.Invoke(type, this.count);
		}

		public void AddEvent(UnityAction<GameDefine.TurnProgressType, int> action)
		{
			this.progressEvent.AddListener(action);
		}
	}
}