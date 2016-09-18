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

		private CellController[,] cells = new CellController[CulumnMax, CulumnMax];

		private const int CulumnMax = 8;

		private const int CellMax = CulumnMax * CulumnMax;

		protected override void Awake()
		{
			base.Awake();
		}

		void Start()
		{
			var database = new CellData[CulumnMax, CulumnMax];
			for(int y = 0; y < CulumnMax; y++)
			{
				for(int x = 0; x < CulumnMax; x++)
				{
					this.cells[y, x] = Instantiate(this.cellPrefab, this.cellField, false) as CellController;
					database[y, x] = this.CreateDebugCellData(y, x);
				}
			}
			this.SetCell(database);

			var initialCell = database[Random.Range(0, CulumnMax), Random.Range(0, CulumnMax)];
			initialCell.Steppable();
			initialCell.Identification();
		}

		public void SetCell(CellData[,] database)
		{
			for(int i = 0; i < CulumnMax; i++)
			{
				for(int j = 0; j < CulumnMax; j++)
				{
					this.cells[i, j].SetCellData(database[i, j]);
				}
			}
		}

		public CellData GetAdjacentCellData(int y, int x, GameDefine.AdjacentType type)
		{
			switch(type)
			{
			case GameDefine.AdjacentType.Left:
				return x <= 0 ? null : cells[y, x - 1].Data;
			case GameDefine.AdjacentType.LeftTop:
				return (x <= 0 || y <= 0) ? null : cells[y - 1, x - 1].Data;
			case GameDefine.AdjacentType.Top:
				return y <= 0 ? null : cells[y - 1, x].Data;
			case GameDefine.AdjacentType.RightTop:
				return (x >= CulumnMax - 1 || y <= 0) ? null : cells[y - 1, x + 1].Data;
			case GameDefine.AdjacentType.Right:
				return x >= CulumnMax - 1 ? null : cells[y, x + 1].Data;
			case GameDefine.AdjacentType.RightBottom:
				return (x >= CulumnMax - 1 || y >= CulumnMax - 1) ? null : cells[y + 1, x + 1].Data;
			case GameDefine.AdjacentType.Bottom:
				return y >= CulumnMax - 1 ? null : cells[y + 1, x].Data;
			case GameDefine.AdjacentType.LeftBottom:
				return (x <= 0 || y >= CulumnMax - 1) ? null : cells[y + 1, x - 1].Data;
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

		private CellData CreateDebugCellData(int y, int x)
		{
			var cellData = new CellData(y, x);
			if(Random.value < 0.2f)
			{
				cellData.BindIdentificationAction(new CreateRecoveryItemAction());
			}

			return cellData;
		}
	}
}