using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class Range
	{
		public int min;

		public int max;

		public int Random
		{
			get
			{
				return UnityEngine.Random.Range(this.min, this.max + 1);
			}
		}
	}
}