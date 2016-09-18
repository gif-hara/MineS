using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CellManager : SingletonMonoBehaviour<CellManager>
	{
		[SerializeField]
		private Transform cellField;

		[SerializeField]
		private CellController cellPrefab;

		private CellController[,] cellControllers = new CellController[RowMax, CulumnMax];

		private CellData[,] cellDatabase;

		private const int RowMax = 10;

		private const int CulumnMax = 8;

		private const int CellMax = CulumnMax * CulumnMax;

		protected override void Awake()
		{
			base.Awake();
		}

		void Start()
		{
			var database = this.CreateDebugCellDatabase();
			for(int y = 0; y < RowMax; y++)
			{
				for(int x = 0; x < CulumnMax; x++)
				{
					this.cellControllers[y, x] = Instantiate(this.cellPrefab, this.cellField, false) as CellController;
				}
			}
			this.SetCell(database);

			this.InitializeStep();

			DungeonManager.Instance.AddNextFloorEvent(this.NextFloor);
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				for(int y = 0; y < RowMax; y++)
				{
					for(int x = 0; x < CulumnMax; x++)
					{
						this.cellControllers[y, x].DebugAction();
					}
				}
			}
		}

		public void SetCell(CellData[,] database)
		{
			this.cellDatabase = database;
			for(int y = 0; y < RowMax; y++)
			{
				for(int x = 0; x < CulumnMax; x++)
				{
					this.cellControllers[y, x].SetCellData(database[y, x]);
				}
			}
			this.CheckCellData(this.cellDatabase);
		}

		private void NextFloor()
		{
			this.SetCell(this.CreateDebugCellDatabase());
			this.InitializeStep();
		}

		public CellData GetAdjacentCellData(int y, int x, GameDefine.AdjacentType type)
		{
			switch(type)
			{
			case GameDefine.AdjacentType.Left:
				return x <= 0 ? null : cellControllers[y, x - 1].Data;
			case GameDefine.AdjacentType.LeftTop:
				return (x <= 0 || y <= 0) ? null : cellControllers[y - 1, x - 1].Data;
			case GameDefine.AdjacentType.Top:
				return y <= 0 ? null : cellControllers[y - 1, x].Data;
			case GameDefine.AdjacentType.RightTop:
				return (x >= CulumnMax - 1 || y <= 0) ? null : cellControllers[y - 1, x + 1].Data;
			case GameDefine.AdjacentType.Right:
				return x >= CulumnMax - 1 ? null : cellControllers[y, x + 1].Data;
			case GameDefine.AdjacentType.RightBottom:
				return (x >= CulumnMax - 1 || y >= RowMax - 1) ? null : cellControllers[y + 1, x + 1].Data;
			case GameDefine.AdjacentType.Bottom:
				return y >= RowMax - 1 ? null : cellControllers[y + 1, x].Data;
			case GameDefine.AdjacentType.LeftBottom:
				return (x <= 0 || y >= RowMax - 1) ? null : cellControllers[y + 1, x - 1].Data;
			}

			return null;
		}

		public List<CellData> GetAdjacentCellDataAll(int y, int x)
		{
			var result = new List<CellData>();
			for(int i = 0; i < GameDefine.AdjacentMax; i++)
			{
				var cell = GetAdjacentCellData(y, x, (GameDefine.AdjacentType)i);
				if(cell == null)
				{
					continue;
				}

				result.Add(cell);
			}

			return result;
		}

		public List<CellData> GetAdjacentCellDataLeftTopRightBottom(int y, int x)
		{
			var result = new List<CellData>();
			result.Add(GetAdjacentCellData(y, x, GameDefine.AdjacentType.Left));
			result.Add(GetAdjacentCellData(y, x, GameDefine.AdjacentType.Top));
			result.Add(GetAdjacentCellData(y, x, GameDefine.AdjacentType.Right));
			result.Add(GetAdjacentCellData(y, x, GameDefine.AdjacentType.Bottom));
			result.RemoveAll(c => c == null);

			return result;
		}

		private void InitializeStep()
		{
			int y, x;
			this.GetBlankCellIndex(this.cellDatabase, out y, out x);
			var initialCell = this.cellDatabase[y, x];
			initialCell.Steppable();
			initialCell.Identification();
		}

		private CellData CreateDebugCellData(int y, int x)
		{
			var cellData = new CellData(y, x);
			if(Random.value < 0.2f)
			{
				cellData.BindCellClickAction(new CreateRecoveryItemAction());
			}
			if(Random.value < 0.2f)
			{
				cellData.BindCellClickAction(new CreateEnemyAction());
			}

			return cellData;
		}

		private CellData[,] CreateDebugCellDatabase()
		{
			var database = new CellData[RowMax, CulumnMax];
			CellData cellData = null;
			int y, x;

			// 階段を作成.
			this.GetNullCellIndex(database, out y, out x);
			cellData = new CellData(y, x);
			cellData.BindCellClickAction(new CreateStairAction());
			database[y, x] = cellData;

			// 回復アイテムを作成.
			for(int i = 0, imax = Random.Range(3, 5); i < imax; i++)
			{
				this.GetNullCellIndex(database, out y, out x);
				cellData = new CellData(y, x);
				cellData.BindCellClickAction(new CreateRecoveryItemAction());
				database[y, x] = cellData;
			}

			// 敵を作成.
			for(int i = 0, imax = Random.Range(4, 6); i < imax; i++)
			{
				this.GetNullCellIndex(database, out y, out x);
				cellData = new CellData(y, x);
				cellData.BindCellClickAction(new CreateEnemyAction());
				database[y, x] = cellData;
			}

			for(y = 0; y < RowMax; y++)
			{
				for(x = 0; x < CulumnMax; x++)
				{
					if(database[y, x] != null)
					{
						continue;
					}

					database[y, x] = new CellData(y, x);
				}
			}

			return database;
		}

		private void CheckCellData(CellData[,] database)
		{
			var existStair = false;
			for(int y = 0; y < RowMax; y++)
			{
				for(int x = 0; x < CulumnMax; x++)
				{
					if(database[y, x].CurrentEventType == GameDefine.EventType.Stair)
					{
						existStair = true;
						break;
					}
				}
			}

			Debug.AssertFormat(existStair, "階段がありませんでした.");
		}

		private void GetNullCellIndex(CellData[,] database, out int y, out int x)
		{
			do
			{
				y = Random.Range(0, RowMax);
				x = Random.Range(0, CulumnMax);
			} while(database[y, x] != null);
		}

		private void GetBlankCellIndex(CellData[,] database, out int y, out int x)
		{
			do
			{
				y = Random.Range(0, RowMax);
				x = Random.Range(0, CulumnMax);
			} while(database[y, x].CurrentEventType != GameDefine.EventType.None);
		}
	}
}