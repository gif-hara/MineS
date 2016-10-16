using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using DG.Tweening;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DoTweenAlphaCanvasGroup : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup target;

		[SerializeField]
		private float from;

		[SerializeField]
		private float to;

		[SerializeField]
		private float duration;

		[SerializeField]
		private Ease ease;

		void Start()
		{
			this.target.alpha = this.from;
			DOTween.To(() => this.from, x => this.target.alpha = x, this.to, this.duration).SetEase(this.ease);
		}
	}
}