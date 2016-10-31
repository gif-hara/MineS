using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class TitleFlowController : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup canvasGroup;

		private UnityEvent startFadeOutEvent = new UnityEvent();

		IEnumerator Start()
		{
			while(Application.isShowingSplashScreen)
			{
				yield return 0;
			}

			this.FadeOut();
		}

		public void AddStartFadeOutEvent(UnityAction call)
		{
			this.startFadeOutEvent.AddListener(call);
		}

		private void FadeOut()
		{
			DOTween.ToAlpha(() => new Color(1.0f, 1.0f, 1.0f, this.canvasGroup.alpha), (x) => this.canvasGroup.alpha = x.a, 0.0f, 0.5f)
				.SetDelay(1.0f)
				.OnStart(() => startFadeOutEvent.Invoke())
				.OnComplete(() => this.canvasGroup.gameObject.SetActive(false));
		}
	}
}