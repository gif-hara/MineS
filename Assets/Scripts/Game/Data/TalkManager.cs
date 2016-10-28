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

		private UnityEvent onEndTalkOnce = new UnityEvent();

		public void StartTalk(TalkChunkData chunkData, UnityAction onceCall)
		{
			this.chunkData = chunkData;
			this.talkedCount = -1;
			this.Next();
			if(onceCall != null)
			{
				this.onEndTalkOnce.AddListener(onceCall);
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
			this.onEndTalkOnce.Invoke();
			this.onEndTalkOnce.RemoveAllListeners();
			this.onEndTalkResident.Invoke();
		}
	}
}