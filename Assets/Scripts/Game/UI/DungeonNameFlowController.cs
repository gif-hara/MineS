using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DungeonNameFlowController : MonoBehaviour
	{
		[SerializeField]
		private Image inputBlock;

		[SerializeField]
		private Image fade;

		[SerializeField]
		private Text text;

		[SerializeField]
		private StringAsset.Finder dungeonFormat;

		[SerializeField]
		private StringAsset.Finder fixDungeonFormat;

		private UnityEvent completeFadeOutEvent = new UnityEvent();

		private UnityEvent startTextFadeInEvent = new UnityEvent();

		private UnityEvent completeFadeInEvent = new UnityEvent();

		void Start()
		{
			this.inputBlock.enabled = false;
			this.fade.enabled = false;
			this.text.enabled = false;
			this.fade.color = new Color(this.fade.color.r, this.fade.color.g, this.fade.color.b, 0.0f);
			this.text.color = new Color(this.text.color.r, this.text.color.g, this.text.color.b, 0.0f);
		}

		public void AddCompleteFadeOutEvent(UnityAction call)
		{
			this.completeFadeOutEvent.AddListener(call);
		}

		public void RemoveCompleteFadeOutEvent(UnityAction call)
		{
			this.completeFadeOutEvent.RemoveListener(call);
		}

		public void AddCompleteFadeInEvent(UnityAction call)
		{
			this.completeFadeInEvent.AddListener(call);
		}

		public void AddStartTextFadeIn(UnityAction call)
		{
			this.startTextFadeInEvent.AddListener(call);
		}

		public void StartFadeOut(DungeonDataBase dungeonData, string dungeonName, int floor, bool immediateFadeOut)
		{
			this.inputBlock.enabled = true;
			this.fade.enabled = true;
			this.text.text = (dungeonData as DungeonData) != null ? this.dungeonFormat.Format(dungeonName, floor) : this.fixDungeonFormat.Format(dungeonName);
			DOTween.ToAlpha(() => this.fade.color, x =>
			{
				if(immediateFadeOut)
				{
					this.fade.color = new Color(x.r, x.g, x.b, 1.0f);
				}
				else
				{
					this.fade.color = x;
				}
			}, 1.0f, 0.5f)
				.OnComplete(() =>
			{
				this.StartFadeInText();
				this.completeFadeOutEvent.Invoke();
			});
		}

		private void StartFadeInText()
		{
			DOTween.ToAlpha(() => this.text.color, x => this.text.color = x, 1.0f, 0.5f)
				.SetDelay(1.0f)
				.OnStart(() =>
			{
				this.text.enabled = true;
				this.startTextFadeInEvent.Invoke();
			})
				.OnComplete(() =>
			{
				this.StartFadeInFade();
			});
		}

		private void StartFadeInFade()
		{
			DOTween.ToAlpha(() => this.fade.color, x => this.fade.color = x, 0.0f, 1.0f)
				.SetDelay(1.0f)
				.OnStart(() =>
			{
				DOTween.ToAlpha(() => this.text.color, x => this.text.color = x, 0.0f, 1.0f)
						.OnComplete(() =>
				{
					this.text.enabled = false;
				});
			})
				.OnComplete(() =>
			{
				this.inputBlock.enabled = false;
				this.fade.enabled = false;
				this.completeFadeInEvent.Invoke();
			});
		}
	}
}