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
	public class ItemTable
	{
		[SerializeField]
		private List<Element> elements;

		public Item Create()
		{
			return this.elements[GameDefine.Lottery(this.elements)].Create();
		}

		[System.Serializable]
		private class Element : IProbability
		{
			[SerializeField]
			private ItemDataBase masterData;

			[SerializeField]
			private int probability;

			public ItemDataBase MasterData{ get { return this.masterData; } }

			public int Probability{ get { return this.probability; } }

			public Item Create()
			{
				return new Item(this.masterData);
			}
		}

	}
}