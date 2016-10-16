using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AutoDestroyDelay : MonoBehaviour
	{
		[SerializeField]
		private float delay;

		void Start()
		{
			Destroy(this.gameObject, this.delay);
		}
	}
}