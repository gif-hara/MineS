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
	public class DungeonSerializeData
	{
		[SerializeField]
		public int floor;

		[SerializeField]
		public DungeonDataBase dungeonData;

		public DungeonSerializeData()
		{
			
		}

		public DungeonSerializeData(int floor, DungeonDataBase dungeonData)
		{
			this.floor = floor;
			this.dungeonData = dungeonData;
		}
	}
}