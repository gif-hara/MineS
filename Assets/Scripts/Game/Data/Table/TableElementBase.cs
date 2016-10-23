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
		private Range floorRange;

		public bool IsMatch(int floor)
		{
			return floor >= this.floorRange.min && floor <= this.floorRange.max;
		}

	}
}