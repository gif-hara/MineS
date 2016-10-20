using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class FixDungeonCellCreatorBase : ScriptableObject
	{
		public abstract CellData Create(int y, int x, CellController cellController);
	}
}