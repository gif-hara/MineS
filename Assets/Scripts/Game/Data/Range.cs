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

		public Range(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		public int Random
		{
			get
			{
				return UnityEngine.Random.Range(this.min, this.max + 1);
			}
		}

		public bool Equals(Range obj)
		{
			return this.min == obj.min && this.max == obj.max;
		}
	}
}