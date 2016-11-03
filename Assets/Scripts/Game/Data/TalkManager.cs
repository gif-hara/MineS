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
	public class TalkManager : SingletonMonoBehaviour<TalkManager>
	{
		private TalkChunkData chunkData;

		private int talkedCount = 0;

		private UnityEvent onEndTalkResident = new UnityEvent();

		private UnityAction onEndTalkOnce;

		public void StartTalk(TalkChunkData chunkData, UnityAction onceCall)
		{
			InformationManager.RemoveAllElement();
			this.chunkData = chunkData;
			this.talkedCount = -1;
			this.Next();
			if(onceCall != null)
			{
				this.onEndTalkOnce = onceCall;
			}
		}

		public void AddEndTalkResident(UnityAction call)
		{
			this.onEndTalkResident.AddListener(call);
		}

		public void Next()
		{
			this.talkedCount++;
			var element = this.chunkData.Get(this.talkedCount);
			if(element == null)
			{
				this.EndTalk();
			}
			else
			{
				element.Invoke();
			}
		}

		private void EndTalk()
		{
			if(this.onEndTalkOnce != null)
			{
				var _onEndTalkOnce = new UnityAction(this.onEndTalkOnce.Invoke);
				this.onEndTalkOnce = null;
				_onEndTalkOnce.Invoke();
			}

			this.onEndTalkResident.Invoke();
		}
	}
}