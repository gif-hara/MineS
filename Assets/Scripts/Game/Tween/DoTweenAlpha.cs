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
	public class DoTweenAlpha : MonoBehaviour
	{
		[SerializeField]
		private Graphic target;

		[SerializeField]
		private float from;

		[SerializeField]
		private float to;

		[SerializeField]
		private float duration;

		[SerializeField]
		private Ease ease;

		[SerializeField]
		private int loopCount = 1;

		void Start()
		{
			this.target.color = new Color(this.target.color.r, this.target.color.g, this.target.color.b, this.from);
			DOTween.ToAlpha(() => this.target.color, x => this.target.color = x, this.to, this.duration)
				.SetEase(this.ease)
				.SetLoops(this.loopCount);
		}
	}
}