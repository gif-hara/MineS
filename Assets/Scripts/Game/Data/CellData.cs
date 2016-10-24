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
	public sealed class CellData
	{
		public int X{ private set; get; }

		public int Y{ private set; get; }

		public int MapChipId{ private set; get; }

		public bool CanStep{ private set; get; }

		public bool IsIdentification{ private set; get; }

		private int lockCount = 0;

		public CellController Controller{ private set; get; }

		private CellClickActionBase cellClickAction;

		private DeployDescriptionBase deployDescription;

		private System.Action<GameDefine.ActionableType> infeasibleEvent = null;

		private System.Action<bool> modifiedCanStepEvent = null;

		private System.Action<bool> modifiedIdentificationEvent = null;

		private System.Action<int> modifiedLockCountEvent = null;

		public CellData(CellController cellController)
		{
			this.X = -1;
			this.Y = -1;
			this.IsIdentification = true;
			this.lockCount = 0;
			this.CanStep = true;
			this.Controller = cellController;
		}

		public CellData(int y, int x, int mapChipId, CellController cellController)
		{
			this.X = x;
			this.Y = y;
			this.MapChipId = mapChipId;
			this.IsIdentification = false;
			this.lockCount = 0;
			this.Controller = cellController;
		}

		public void Setup()
		{
			if(this.cellClickAction != null)
			{
				this.cellClickAction.SetCellController(this.Controller);
				this.cellClickAction.SetCellData(this);
			}
		}

		public void Action()
		{
			var actionableType = this.GetActionableType;
			if(actionableType != GameDefine.ActionableType.OK && this.infeasibleEvent != null)
			{
				this.infeasibleEvent(actionableType);
				return;
			}

			var isIdentification = this.Identification(true, PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray), true);
			if(this.cellClickAction != null)
			{
				var currentCellClickAction = this.cellClickAction;
				currentCellClickAction.Invoke(this);
				if(isIdentification)
				{
					currentCellClickAction.OnIdentification(this);
				}
			}

			if(isIdentification)
			{
				TurnManager.Instance.Progress(GameDefine.TurnProgressType.CellClick);
				Object.Instantiate(EffectManager.Instance.prefabStepEffect.Element, this.Controller.transform, false);
			}
		}

		public void DebugAction()
		{
			this.Identification(true, false, false);
			if(this.cellClickAction != null)
			{
				this.cellClickAction.Invoke(this);
			}
		}

		public void Description()
		{
			if(this.deployDescription == null)
			{
				return;
			}

			this.deployDescription.Deploy();
		}

		public void OnUseXray()
		{
			if(this.cellClickAction == null)
			{
				return;
			}

			this.cellClickAction.OnUseXray();
		}

		public void BindEvent(
			System.Action<GameDefine.ActionableType> infeasibleEvent,
			System.Action<bool> modifiedCanStepEvent,
			System.Action<bool> modifiedIdentificationEvent, 
			System.Action<int> modifiedLockCountEvent
		)
		{
			this.infeasibleEvent = infeasibleEvent;
			this.modifiedCanStepEvent = modifiedCanStepEvent;
			this.modifiedIdentificationEvent = modifiedIdentificationEvent;
			this.modifiedLockCountEvent = modifiedLockCountEvent;

			this.modifiedCanStepEvent(this.CanStep);
			this.modifiedIdentificationEvent(this.IsIdentification);
			this.modifiedLockCountEvent(this.lockCount);
		}

		public void BindCellClickAction(CellClickActionBase cellClickAction)
		{
			this.cellClickAction = cellClickAction;
		}

		public void BindDeployDescription(DeployDescriptionBase deployDescription)
		{
			this.deployDescription = deployDescription;
		}

		public void Steppable(bool isXray)
		{
			if(this.CanStep)
			{
				return;
			}

			if(isXray)
			{
				this.OnUseXray();
			}

			this.CanStep = true;
			if(this.modifiedCanStepEvent != null)
			{
				this.modifiedCanStepEvent(this.CanStep);
			}
		}

		public void AddLock()
		{
			this.lockCount++;
			if(this.modifiedLockCountEvent != null)
			{
				this.modifiedLockCountEvent(this.lockCount);
			}
		}

		public void ReleaseLock()
		{
			this.lockCount--;
			if(this.modifiedLockCountEvent != null)
			{
				this.modifiedLockCountEvent(this.lockCount);
			}
		}

		public bool Identification(bool isSteppableAdjacentCells, bool isXray, bool playSE)
		{
			if(this.IsIdentification)
			{
				return false;
			}

			if(isSteppableAdjacentCells)
			{
				var adjacentCells = CellManager.Instance.GetAdjacentCellDataLeftTopRightBottom(this.Y, this.X);
				for(int i = 0; i < adjacentCells.Count; i++)
				{
					adjacentCells[i].Steppable(isXray);
				}
			}

			this.IsIdentification = true;
			if(this.modifiedIdentificationEvent != null)
			{
				this.modifiedIdentificationEvent(this.IsIdentification);
			}

			if(playSE)
			{
				var seManager = SEManager.Instance;
				seManager.PlaySE(seManager.walks[Random.Range(0, seManager.walks.Count)]);
			}

			return true;
		}

		public List<CellData> AdjacentCellAll
		{
			get
			{
				return CellManager.Instance.GetAdjacentCellDataAll(this.Y, this.X);
			}
		}

		public bool IsLock
		{
			get
			{
				return this.lockCount > 0;
			}
		}

		public GameDefine.EventType CurrentEventType
		{
			get
			{
				if(this.cellClickAction == null)
				{
					return GameDefine.EventType.None;
				}

				return this.cellClickAction.EventType;
			}
		}

		public GameDefine.ActionableType GetActionableType
		{
			get
			{
				if(!this.CanStep)
				{
					return GameDefine.ActionableType.NotStep;
				}
				if(this.IsLock)
				{
					return GameDefine.ActionableType.Lock;
				}

				return GameDefine.ActionableType.OK;
			}
		}
	}
}