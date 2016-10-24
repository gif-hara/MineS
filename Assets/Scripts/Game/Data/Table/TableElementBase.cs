using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class TableElementBase
	{
		[SerializeField]
		protected Range floorRange;

		public TableElementBase(Range floorRange)
		{
			this.floorRange = floorRange;
		}

		public bool IsMatch(int floor)
		{
			return floor >= this.floorRange.min && floor <= this.floorRange.max;
		}

#if UNITY_EDITOR
		public bool IsMatchRange(Range range)
		{
			return this.floorRange.Equals(range);
		}
#endif
	}
}