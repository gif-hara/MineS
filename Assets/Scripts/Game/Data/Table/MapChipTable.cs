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
	public class MapChipTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField]
			private MapChipData mapChip;

			public MapChipData MapChip{ get { return this.mapChip; } }
		}

		[SerializeField]
		private List<Element> elements;

		public MapChipData Get(int floor)
		{
			return this.elements.Find(e => e.IsMatch(floor)).MapChip;
		}
	}
}