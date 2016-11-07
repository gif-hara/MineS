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
			var _dungeonData = dungeonData as FixDungeonData;
			var database = new Database(rowNumber, culumnNumber);
			var mapChipCreator = new FixDungeonMapChipCreator(_dungeonData, DungeonManager.Instance.Floor);

			// 街データを生成.
			_dungeonData.CellCreators.ForEach(c => database.Set(database.Pop(c.Y, c.X), c.Create(cellManager.CellControllers[c.Y, c.X], mapChipCreator)));

			// 空白を作成.
			for(int i = 0, imax = database.RestCount; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, null, 0);
			}

			if(_dungeonData.AllIdentification)
			{
				for(int y = 0; y < rowNumber; y++)
				{
					for(int x = 0; x < culumnNumber; x++)
					{
						database.CellDatabase[y, x].Steppable(false);
						database.CellDatabase[y, x].Identification(false, false, false);
					}
				}
			}

			return database.CellDatabase;
		}
	}
}