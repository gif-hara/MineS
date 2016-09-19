using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class Gauge : MonoBehaviour
	{
		[SerializeField]
		private RectTransform valueTransform;

		private RectTransform root;

		void Start()
		{
			this.root = this.transform as RectTransform;
		}

		public void Set(float value)
		{
			this.valueTransform.sizeDelta = new Vector2(-this.root.sizeDelta.x * (1.0f - value), this.valueTransform.sizeDelta.y);
		}
	}
}