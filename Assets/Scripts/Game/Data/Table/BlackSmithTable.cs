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
	public class BlackSmithTable
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

			public bool IsMatchFloor(int floor)
			{
				return floor >= this.floorMin && floor <= this.floorMax;
			}

			public bool CanCreate
			{
				get
				{
					return this.probability > Random.Range(0, 100);
				}
			}
		}

		[SerializeField]
		private List<Element> elements;

		public bool CanCreate(int floor)
		{
			var element = this.elements.Find(e => e.IsMatchFloor(floor));
			if(element == null)
			{
				return false;
			}

			return element.CanCreate;
		}
	}
}