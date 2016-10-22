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
	public class DungeonCreator : DungeonCreatorBase
	{
		public override CellData[,] Create(CellManager cellManager, DungeonDataBase dungeonData, int rowNumber, int culumnNumber)
		{
			var dungeonManager = DungeonManager.Instance;
			var _dungeonData = dungeonData as DungeonData;
			var database = new Database(rowNumber, culumnNumber);
			var mapChipCreator = new RandomMapChipCreator(rowNumber, culumnNumber);

			// 階段を作成.
			this.CreateCellData(cellManager, database, mapChipCreator, new CreateStairAction());

			// 回復アイテムを作成.
			for(int i = 0, imax = _dungeonData.CreateRecoveryItemRange.Random; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateRecoveryItemAction());
			}

			// 鉄床を作成.
			for(int i = 0, imax = _dungeonData.CreateAnvilRange.Random; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateAnvilAction());
			}

			// 金袋を作成.
			for(int i = 0, imax = _dungeonData.CreateMoneyRange.Random; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateMoneyAction());
			}

			// 敵を作成.
			for(int i = 0, imax = _dungeonData.CreateEnemyRange.Random; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateEnemyAction());
			}

			// アイテムを作成.
			for(int i = 0, imax = _dungeonData.CreateItemRange.Random; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateItemAction(_dungeonData.CreateItem()));
			}

			// 罠を作成.
			for(int i = 0, imax = _dungeonData.CreateTrapRange.Random; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, _dungeonData.CreateTrap());
			}

			// 鍛冶屋を作成.
			if(_dungeonData.CanCreateBlackSmith(dungeonManager.Floor))
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateBlackSmithAction());
			}

			// 店を作成.
			if(_dungeonData.CanCreateShop(dungeonManager.Floor))
			{
				this.CreateCellData(cellManager, database, mapChipCreator, new CreateShopAction());
			}

			// 空白を作成.
			for(int i = 0, imax = database.Rest; i < imax; i++)
			{
				this.CreateCellData(cellManager, database, mapChipCreator, null, 0);
			}

			return database.CellDatabase;
		}
	}
}