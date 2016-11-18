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

		private ProgressEvent endTurnProgressEvent = new ProgressEvent();

		private ProgressEvent lateEndTurnProgressEvent = new ProgressEvent();

		private int count = 0;

		public void Progress(GameDefine.TurnProgressType type)
		{
			this.count++;
			this.endTurnProgressEvent.Invoke(type, this.count);
			this.lateEndTurnProgressEvent.Invoke(type, this.count);
			Calculator.ResetCanInvokeSummon();
			DungeonManager.Instance.Serialize();
		}

		public void AddEndTurnEvent(UnityAction<GameDefine.TurnProgressType, int> action)
		{
			this.endTurnProgressEvent.AddListener(action);
		}

		public void AddLateEndTurnEvent(UnityAction<GameDefine.TurnProgressType, int> action)
		{
			this.lateEndTurnProgressEvent.AddListener(action);
		}
	}
}