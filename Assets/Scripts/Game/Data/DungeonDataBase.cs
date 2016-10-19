using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class DungeonDataBase : ScriptableObject
	{
		public abstract CellData[,] Create(CellManager cellManager, DungeonManager dungeonManager);
	}
}