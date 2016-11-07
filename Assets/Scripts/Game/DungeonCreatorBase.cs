using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class DungeonCreatorBase
	{
		public class Database
		{
			private List<Cell> data;

			public CellData[,] CellDatabase{ private set; get; }

			public Database(int rowNumber, int culumnNumber)
			{
				this.data = new List<Cell>();
				this.CellDatabase = new CellData[rowNumber, culumnNumber];
				for(int y = 0; y < rowNumber; y++)
				{
					for(int x = 0; x < culumnNumber; x++)
					{
						this.data.Add(new Cell(y, x));
					}
				}
			}

			public Cell Pop(int index)
			{
				var cell = this.data[index];
				this.data.Remove(cell);
				return cell;
			}

			public Cell Seek(int index)
			{
				return this.data[index];
			}

			public Cell Pop(int y, int x)
			{
				var cell = this.data.Find(c => c.y == y && c.x == x);
				this.data.Remove(cell);
				return cell;
			}

			public void Set(Cell cell, CellData cellData)
			{
				this.CellDatabase[cell.y, cell.x] = cellData;
			}

			public int RestCount
			{
				get
				{
					return this.data.Count;
				}
			}
		}

		public abstract CellData[,] Create(CellManager cellManager, DungeonDataBase dungeonData, int rowNumber, int culumnNumber);

		protected void CreateCellData(CellManager cellManager, Database database, MapChipCreatorBase mapChipCreator, CellClickActionBase action, int cellIndex)
		{
			var cell = database.Pop(cellIndex);
			var cellData = new CellData(cell.y, cell.x, mapChipCreator.Get(cell.y, cell.x), cellManager.CellControllers[cell.y, cell.x]);
			cellData.BindCellClickAction(action);
			database.Set(cell, cellData);
		}

		protected void CreateCellData(CellManager cellManager, Database database, MapChipCreatorBase mapChipCreator, CellClickActionBase action, int y, int x)
		{
			var cell = database.Pop(y, x);
			var cellController = cellManager.CellControllers[cell.y, cell.x];
			var cellData = new CellData(cell.y, cell.x, mapChipCreator.Get(y, x), cellController);

			cellData.BindCellClickAction(action);
			database.Set(cell, cellData);
		}

		protected void CreateCellData(CellManager cellManager, Database database, MapChipCreatorBase mapChipCreator, CellClickActionBase action)
		{
			this.CreateCellData(cellManager, database, mapChipCreator, action, Random.Range(0, database.RestCount));
		}
	}
}