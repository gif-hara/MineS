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

		[SerializeField]
		private float size;

		public void Set(float value)
		{
			this.valueTransform.sizeDelta = new Vector2(-this.size * (1.0f - value), this.valueTransform.sizeDelta.y);
		}
	}
}