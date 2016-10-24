using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class FixDungeonMapChipCreator : MapChipCreatorBase
	{
		private List<List<int>> ids;

		public FixDungeonMapChipCreator(FixDungeonData data)
		{
			this.ids = new List<List<int>>();
			var splitMapChip = data.MapChip.Split('\n');
			foreach(var s in splitMapChip)
			{
				var id = new List<int>();
				for(int i = 0; i < s.Length; i++)
				{
					int value;
					if(int.TryParse(s[i].ToString(), out value))
					{
						id.Add(value);
					}
				}
				this.ids.Add(id);
			}
		}

		public override int Get(int y, int x)
		{
			return this.ids[y][x];
		}
	}
}