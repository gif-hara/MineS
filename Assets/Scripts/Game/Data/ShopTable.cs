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
	public class ShopTable
	{
		[System.Serializable]
		public class Element
		{
			[SerializeField]
			private int floorMin;

			[SerializeField]
			private int floorMax;

			[SerializeField][Range(0, 100)]
			private int probability;

			[SerializeField]
			private ItemTable itemTable;

			public bool IsMatchFloor(int floor)
			{
				return floor >= this.floorMin && floor <= this.floorMax;
			}
		}

		[SerializeField]
		private List<Element> elements;

		public bool CanCreate(int floor)
		{
			return this.elements.Exists(e => e.IsMatchFloor(floor));
		}
	}
}