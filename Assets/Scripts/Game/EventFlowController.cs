using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;
using System.Collections;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EventFlowController : SingletonMonoBehaviour<EventFlowController>
	{
		[SerializeField]
		private GameObject inputBlock;

		private Queue<System.Action> actionList = new Queue<System.Action>();

		public void AddAction(System.Action action)
		{
			if(this.actionList.Count <= 0)
			{
				action.Invoke();
				this.inputBlock.SetActive(true);
			}
			else
			{
				this.actionList.Enqueue(action);
			}
		}

		public void Complete()
		{
			if(this.actionList.Count > 0)
			{
				StartCoroutine(this.CompleteCoroutine());
			}
			else
			{
				this.inputBlock.SetActive(false);
			}
		}

		private IEnumerator CompleteCoroutine()
		{
			yield return new WaitForSecondsRealtime(0.5f);

			var action = this.actionList.Dequeue();
			action.Invoke();
		}
	}
}