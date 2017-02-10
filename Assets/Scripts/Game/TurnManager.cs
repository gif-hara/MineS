using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;
using UniRx;

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

		public IntReactiveProperty Count { private set; get; }

	    void Awake()
	    {
	        this.Count = new IntReactiveProperty();
	    }

		public void Progress(GameDefine.TurnProgressType type)
		{
			this.Count.Value++;
			this.endTurnProgressEvent.Invoke(type, this.Count.Value);
			this.lateEndTurnProgressEvent.Invoke(type, this.Count.Value);
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