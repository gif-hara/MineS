using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

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

		public CellController[,] CellControllers{ private set; get; }

		private CellData[,] cellDatabase;

		public GameDefine.CellClickMode ClickMode{ private set; get; }

		public CellData[,] CellDatabase{ get { return this.cellDatabase; } }

		void Start()
		{
			this.ClickMode = GameDefine.CellClickMode.Step;
			this.CellControllers = new CellController[GameDefine.CellRowMax, GameDefine.CellCulumnMax];
			for(int y = 0; y < GameDefine.CellRowMax; y++)
			{
				for(int x = 0; x < GameDefine.CellCulumnMax; x++)
				{
					this.CellControllers[y, x] = Instantiate(this.cellPrefab, this.cellField, false) as CellController;
				}
			}

			DungeonManager.Instance.AddNextFloorEvent(this.NextFloor);
			TurnManager.Instance.AddLateEndTurnEvent(this.OnLateTurnProgress);
		}

		private void SetCell(CellData[,] database)
		{
			this.cellDatabase = database;
			for(int y = 0; y < GameDefine.CellRowMax; y++)
			{
				for(int x = 0; x < GameDefine.CellCulumnMax; x++)
				{
					this.CellControllers[y, x].SetCellData(database[y, x]);
				}
			}
			for(int y = 0; y < GameDefine.CellRowMax; y++)
			{
				for(int x = 0; x < GameDefine.CellCulumnMax; x++)
				{
					var dungeonManager = DungeonManager.Instance;
					var centerData = this.CellControllers[y, x].Data;
					var leftData = this.GetAdjacentCellData(y, x, GameDefine.AdjacentType.Left);
					var rightData = this.GetAdjacentCellData(y, x, GameDefine.AdjacentType.Right);
					var topData = this.GetAdjacentCellData(y, x, GameDefine.AdjacentType.Top);
					var bottomData = this.GetAdjacentCellData(y, x, GameDefine.AdjacentType.Bottom);
					this.CellControllers[y, x].SetMapChip(dungeonManager.CurrentData.GetMapChip(dungeonManager.Floor).Get(
						centerData.MapChipId,
						leftData == null ? 1 : leftData.MapChipId,
						rightData == null ? 1 : rightData.MapChipId,
						topData == null ? 1 : topData.MapChipId,
						bottomData == null ? 1 : bottomData.MapChipId
					));

				}
			}
			this.CheckCellData(this.cellDatabase);
		}

		public void ActionFromConfusion()
		{
			var notIdentificationCellControllers = this.TurnProgressableCellControllers;
			notIdentificationCellControllers[Random.Range(0, notIdentificationCellControllers.Count)].ActionFromConfusion();
		}

		public void ChangeCellClickMode(GameDefine.CellClickMode mode)
		{
			this.ClickMode = mode;
		}

		private void NextFloor()
		{
			this.CreateCellDatabaseFromDungeonData();
		}

		private void OnLateTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			var playerData = PlayerManager.Instance.Data;
			if(playerData.FindAbility(GameDefine.AbilityType.Clairvoyance) || playerData.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray))
			{
				this.OnUseXrayNotIdentification();
			}
		}

		public void OnUseXrayNotIdentification()
		{
			var notIdentificationCells = this.ToListCellData.Where(c => !c.IsIdentification && c.CanStep).ToList();
			notIdentificationCells.ForEach(c => c.OnUseXray());
		}

		public void OnUseXrayAll()
		{
			this.ToListCellData.ForEach(c => c.OnUseXray());
		}

		public void RemoveTrap()
		{
			var trapCells = this.ToListCellData.Where(c => c.CurrentEventType == GameDefine.EventType.Trap).ToList();
			trapCells.ForEach(c =>
			{
				c.BindCellClickAction(null);
				c.Controller.SetImage(null);
			});
		}

		public CellData GetAdjacentCellData(int y, int x, GameDefine.AdjacentType type)
		{
			switch(type)
			{
			case GameDefine.AdjacentType.Left:
				return x <= 0 ? null : CellControllers[y, x - 1].Data;
			case GameDefine.AdjacentType.LeftTop:
				return (x <= 0 || y <= 0) ? null : CellControllers[y - 1, x - 1].Data;
			case GameDefine.AdjacentType.Top:
				return y <= 0 ? null : CellControllers[y - 1, x].Data;
			case GameDefine.AdjacentType.RightTop:
				return (x >= GameDefine.CellCulumnMax - 1 || y <= 0) ? null : CellControllers[y - 1, x + 1].Data;
			case GameDefine.AdjacentType.Right:
				return x >= GameDefine.CellCulumnMax - 1 ? null : CellControllers[y, x + 1].Data;
			case GameDefine.AdjacentType.RightBottom:
				return (x >= GameDefine.CellCulumnMax - 1 || y >= GameDefine.CellRowMax - 1) ? null : CellControllers[y + 1, x + 1].Data;
			case GameDefine.AdjacentType.Bottom:
				return y >= GameDefine.CellRowMax - 1 ? null : CellControllers[y + 1, x].Data;
			case GameDefine.AdjacentType.LeftBottom:
				return (x <= 0 || y >= GameDefine.CellRowMax - 1) ? null : CellControllers[y + 1, x - 1].Data;
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

		/// <summary>
		/// 十字方向の隣接するセルを返す.
		/// 例：range = 1
		///  *
		/// ***
		///  *
		/// 例：range = 2
		///   *
		///  ***
		/// *****
		///  ***
		///   *
		/// </summary>
		/// <returns>The adjacent cell data slanting.</returns>
		/// <param name="position">Position.</param>
		/// <param name="range">Range.</param>
		public List<CellData> GetAdjacentCellDataSlanting(Cell position, int range)
		{
			var result = new Dictionary<Cell, CellData>();
			result.Add(position, this.CellDatabase[position.y, position.x]);
			for(int i = 0; i < range; i++)
			{
				var addCell = new List<CellData>();
				foreach(var r in result.Values)
				{
					this.GetAdjacentCellDataLeftTopRightBottom(r.Position.y, r.Position.x).ForEach(c =>
					{
						addCell.Add(c);
					});
				}
				addCell.ForEach(a =>
				{
					if(!result.ContainsKey(a.Position))
					{
						result.Add(a.Position, a);
					}
				});
			}
			return result.Select(r => r.Value).ToList();
		}

		public List<CellData> GetCrossCellDataAll(Cell origin)
		{
			return this.ToListCellData.Where(c => (c.Position.x == origin.x || c.Position.y == origin.y)).ToList();
		}

		public void DebugAction()
		{
			for(int y = 0; y < GameDefine.CellRowMax; y++)
			{
				for(int x = 0; x < GameDefine.CellCulumnMax; x++)
				{
					this.CellControllers[y, x].DebugAction();
				}
			}
		}

		private void InitializeStep()
		{
			var initialCell = this.RandomBlankCell(false);
			if(initialCell == null)
			{
				return;
			}
			var playerData = PlayerManager.Instance.Data;
			var isXray = playerData.IsXray;
			initialCell.Steppable(isXray);
			initialCell.Identification(true, isXray, false);
		}

		public void CreateCellDatabaseFromDungeonData()
		{
			var dungeonData = DungeonManager.Instance.CurrentData;
			this.SetCell(dungeonData.Create(this));
			dungeonData.InitialStep();
		}

		private void CheckCellData(CellData[,] database)
		{
			var existStair = false;
			for(int y = 0; y < GameDefine.CellRowMax; y++)
			{
				for(int x = 0; x < GameDefine.CellCulumnMax; x++)
				{
					if(database[y, x].CurrentEventType == GameDefine.EventType.Stair)
					{
						existStair = true;
						break;
					}
				}
			}

			if(DungeonManager.Instance.CurrentDataAsDungeon != null)
			{
				Debug.AssertFormat(existStair, "階段がありませんでした.");
			}
		}

		/// <summary>
		/// ターン経過可能なセルコントローラーを返す.
		/// </summary>
		/// <value>The turn progressable cell controllers.</value>
		private List<CellController> TurnProgressableCellControllers
		{
			get
			{
				var result = this.ToListCellData;
				return result.Where(c => (c.CanStep && !c.IsLock) && (!c.IsIdentification || c.CurrentEventType == GameDefine.EventType.Enemy)).Select(c => c.Controller).ToList();
			}
		}

		public List<CellData> ToListCellData
		{
			get
			{
				var result = new List<CellData>();
				for(int y = 0; y < GameDefine.CellRowMax; y++)
				{
					for(int x = 0; x < GameDefine.CellCulumnMax; x++)
					{
						result.Add(this.cellDatabase[y, x]);
					}
				}

				return result;
			}
		}

		public CellData RandomBlankCell(bool isIdentification)
		{
			var blankCells = this.ToListCellData;
			blankCells = blankCells.Where(c => (isIdentification == c.IsIdentification) && c.CurrentEventType == GameDefine.EventType.None).ToList();
			if(blankCells.Count <= 0)
			{
				return null;
			}
			return blankCells[Random.Range(0, blankCells.Count)];
		}

		public void Serialize()
		{
			DungeonSerializer.SerializeCellData(this.cellDatabase, GameDefine.CellRowMax, GameDefine.CellCulumnMax);
		}

		public void Deserialize()
		{
			this.cellDatabase = DungeonSerializer.DeserializeCellData(this.CellControllers, GameDefine.CellRowMax, GameDefine.CellCulumnMax);
			this.SetCell(this.cellDatabase);
			this.ToListCellData.ForEach(c => c.UseXrayOnDeserialize());
		}

		public void LateDeserialize()
		{
			this.ToListCellData.ForEach(c => c.LateDeserialize());
		}
	}
}