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
	public class FixDungeonCreator : DungeonCreatorBase
	{
		public override CellData[,] Create(CellManager cellManager, DungeonDataBase dungeonData, int rowNumber, int culumnNumber)
		{
			var _dungeonData = dungeonData as FixDungeonDungeonData;
			var database = new Database(rowNumber, culumnNumber);

			// 街データを生成.
			_dungeonData.CellCreators.ForEach(c => database.Set(database.Pop(c.Y, c.X), c.Create(cellManager.CellControllers[c.Y, c.X])));

			// 空白を作成.
			for(int i = 0, imax = database.Rest; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, null, 0);
			}

			for(int y = 0; y < rowNumber; y++)
			{
				for(int x = 0; x < culumnNumber; x++)
				{
					database.CellDatabase[y, x].Steppable(false);
					database.CellDatabase[y, x].Identification(false, false);
				}
			}

			return database.CellDatabase;
		}
	}
}