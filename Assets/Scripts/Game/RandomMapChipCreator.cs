using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class RandomMapChipCreator : MapChipCreatorBase
	{
		private int[,] ids;

		public RandomMapChipCreator(int rowMax, int culumnMax)
		{
			this.ids = new int[rowMax, culumnMax];
			for(int i = 0; i < rowMax; i++)
			{
				this.ids[i, 3] = 1;
			}
			for(int i = 0; i < culumnMax; i++)
			{
				this.ids[2, i] = 1;
			}
		}

		public override int Get(int y, int x)
		{
			return this.ids[y, x];
		}
	}
}