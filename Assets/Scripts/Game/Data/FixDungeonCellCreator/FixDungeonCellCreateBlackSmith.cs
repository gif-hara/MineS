using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class FixDungeonCellCreateBlackSmith : FixDungeonCellCreatorBase
	{
		public override CellData Create(int y, int x, CellController cellController)
		{
			var cellData = new CellData(y, x, 0, cellController);
			cellData.BindCellClickAction(new VisitBlackSmithAction());

			return cellData;
		}
	}
}