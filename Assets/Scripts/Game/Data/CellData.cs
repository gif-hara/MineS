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

		private bool canStep = false;

		public bool IsIdentification{ private set; get; }

		private int lockCount = 0;

		public CellController Controller{ private set; get; }

		private CellClickActionBase cellClickAction;

		private DeployDescriptionBase deployDescription;

		private System.Action<GameDefine.ActionableType> infeasibleEvent = null;

		private System.Action<bool> modifiedCanStepEvent = null;

		private System.Action<bool> modifiedIdentificationEvent = null;

		private System.Action<int> modifiedLockCountEvent = null;

		public CellData()
		{
			this.X = -1;
			this.Y = -1;
			this.IsIdentification = true;
			this.lockCount = 0;
			this.canStep = true;
		}

		public CellData(int y, int x)
		{
			this.X = x;
			this.Y = y;
			this.IsIdentification = false;
			this.lockCount = 0;
		}

		public void Action()
		{
			var actionableType = this.GetActionableType;
			if(actionableType != GameDefine.ActionableType.OK && this.infeasibleEvent != null)
			{
				this.infeasibleEvent(actionableType);
				return;
			}

			this.Identification(true, PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray));
			if(this.cellClickAction != null)
			{
				this.cellClickAction.Invoke(this);
			}
		}

		public void DebugAction()
		{
			this.Identification(false, false);
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

		public void SetController(CellController controller)
		{
			this.Controller = controller;
			if(this.cellClickAction != null)
			{
				this.cellClickAction.SetCellController(controller);
			}
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

			this.modifiedCanStepEvent(this.canStep);
			this.modifiedIdentificationEvent(this.IsIdentification);
			this.modifiedLockCountEvent(this.lockCount);
		}

		public void BindCellClickAction(CellClickActionBase cellClickAction)
		{
			this.cellClickAction = cellClickAction;
			if(this.cellClickAction != null)
			{
				this.cellClickAction.SetCellData(this);
			}
		}

		public void BindDeployDescription(DeployDescriptionBase deployDescription)
		{
			this.deployDescription = deployDescription;
		}

		public void Steppable(bool isXray)
		{
			if(this.canStep)
			{
				return;
			}

			if(isXray && this.cellClickAction != null)
			{
				this.cellClickAction.OnUseXray();
			}

			this.canStep = true;
			if(this.modifiedCanStepEvent != null)
			{
				this.modifiedCanStepEvent(this.canStep);
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

		public void Identification(bool progressTurn, bool isXray)
		{
			if(this.IsIdentification)
			{
				return;
			}

			var adjacentCells = CellManager.Instance.GetAdjacentCellDataLeftTopRightBottom(this.Y, this.X);
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				adjacentCells[i].Steppable(isXray);
			}

			this.IsIdentification = true;
			if(this.modifiedIdentificationEvent != null)
			{
				this.modifiedIdentificationEvent(this.IsIdentification);
			}

			if(progressTurn)
			{
				TurnManager.Instance.Progress(GameDefine.TurnProgressType.CellClick);
			}
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
				if(!this.canStep)
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