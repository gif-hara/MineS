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
	public class FixDungeonCellCreateChangeDungeon : FixDungeonCellCreatorBase
	{
		[SerializeField]
		private DungeonDataBase data;

		[SerializeField]
		private string descriptionKey;

		public override CellData Create(int y, int x, CellController cellController)
		{
			var cellData = new CellData(y, x, cellController);
			cellData.BindCellClickAction(new ChangeDungeonDataAction(this.data, this.descriptionKey));

			return cellData;
		}
	}
}