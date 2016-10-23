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
	public class TrapTable
	{
		[SerializeField]
		private List<Element> elements;

		public InvokeTrapActionBase Create()
		{
			return this.elements[GameDefine.Lottery(this.elements)].Create();
		}

		[System.Serializable]
		private class Element : IProbability
		{
			[SerializeField]
			private GameDefine.TrapType type;

			[SerializeField]
			private int probability;

			public int Probability{ get { return this.probability; } }

			public InvokeTrapActionBase Create()
			{
				return InvokeTrapFactory.Create(this.type);
			}
		}

	}
}