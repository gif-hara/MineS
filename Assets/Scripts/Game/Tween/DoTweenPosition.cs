using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using DG.Tweening;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DoTweenPosition : MonoBehaviour
	{
		[SerializeField]
		private Vector3 from;

		[SerializeField]
		private Vector3 to;

		[SerializeField]
		private float duration;

		[SerializeField]
		private Ease ease;

		[SerializeField]
		private Transform target;

		void Start()
		{
			this.target.DOLocalMove(this.to, this.duration).SetEase(this.ease);
		}
	}
}