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
	public class FieldCreator
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

		public CellData[,] Create(DungeonManager dungeonManager, int rowNumber, int culumnNumber)
		{
			var dungeonData = dungeonManager.CurrentData;
			var database = new Database(rowNumber, culumnNumber);

			// 階段を作成.
			this.CreateCellData(database, new CreateStairAction());

			// 回復アイテムを作成.
			for(int i = 0, imax = dungeonData.CreateRecoveryItemRange.Random; i < imax; i++)
			{
				this.CreateCellData(database, new CreateRecoveryItemAction());
			}

			// 敵を作成.
			for(int i = 0, imax = dungeonData.CreateEnemyRange.Random; i < imax; i++)
			{
				this.CreateCellData(database, new CreateEnemyAction());
			}

			// アイテムを作成.
			for(int i = 0, imax = dungeonData.CreateItemRange.Random; i < imax; i++)
			{
				this.CreateCellData(database, new CreateItemAction(dungeonData.CreateItem()));
			}

			// 罠を作成.
			for(int i = 0, imax = dungeonData.CreateTrapRange.Random; i < imax; i++)
			{
				this.CreateCellData(database, dungeonData.CreateTrap());
			}

			// 鍛冶屋を作成.
			if(dungeonData.CanCreateBlackSmith(dungeonManager.Floor))
			{
				this.CreateCellData(database, new CreateBlackSmithAction());
			}

			// 店を作成.
			if(dungeonData.CanCreateShop(dungeonManager.Floor))
			{
				this.CreateCellData(database, new CreateShopAction());
			}

			// 空白を作成.
			for(int i = 0, imax = database.Rest; i < imax; i++)
			{
				this.CreateCellData(database, null, 0);
			}

			return database.CellDatabase;
		}

		private void CreateCellData(Database database, CellClickActionBase action, int cellIndex)
		{
			var cell = database.Pop(cellIndex);
			var cellData = new CellData(cell.y, cell.x);
			cellData.BindCellClickAction(action);
			database.Set(cell, cellData);
		}

		private void CreateCellData(Database database, CellClickActionBase action)
		{
			this.CreateCellData(database, action, Random.Range(0, database.Rest));
		}
	}
}