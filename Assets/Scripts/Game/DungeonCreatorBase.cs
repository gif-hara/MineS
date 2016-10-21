﻿using UnityEngine;
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
		public class Cell
		{
			public int x;
			public int y;

			public Cell(int y, int x)
			{
				this.x = x;
				this.y = y;
			}
		}

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

			public int Rest
			{
				get
				{
					return this.data.Count;
				}
			}
		}

		public abstract CellData[,] Create(CellManager cellManager, DungeonDataBase dungeonData, int rowNumber, int culumnNumber);

		protected void CreateCellData(CellManager cellManager, Database database, CellClickActionBase action, int cellIndex)
		{
			var cell = database.Pop(cellIndex);
			var cellData = new CellData(cell.y, cell.x, cellManager.CellControllers[cell.y, cell.x]);
			cellData.BindCellClickAction(action);
			database.Set(cell, cellData);
		}

		protected void CreateCellData(CellManager cellManager, Database database, CellClickActionBase action, int y, int x)
		{
			var cell = database.Pop(y, x);
			var cellData = new CellData(cell.y, cell.x, cellManager.CellControllers[cell.y, cell.x]);
			cellData.BindCellClickAction(action);
			database.Set(cell, cellData);
		}

		protected void CreateCellData(CellManager cellManager, Database database, CellClickActionBase action)
		{
			this.CreateCellData(cellManager, database, action, Random.Range(0, database.Rest));
		}
	}
}