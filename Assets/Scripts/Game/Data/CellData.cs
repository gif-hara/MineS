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

		private CellClickActionBase identificationAction;

		private CellClickActionBase ridingObjectAction;

		private System.Action<GameDefine.ActionableType> infeasibleEvent = null;

		private System.Action<bool> modifiedCanStepEvent = null;

		private System.Action<bool> modifiedIdentificationEvent = null;

		private System.Action<int> modifiedLockCountEvent = null;

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

			if(!this.IsIdentification)
			{
				this.Identification();
				if(this.identificationAction != null)
				{
					this.identificationAction.Invoke(this);
				}
			}
			else if(this.ridingObjectAction != null)
			{
				this.ridingObjectAction.Invoke(this);
			}
		}

		public void Description()
		{
		}

		public void SetController(CellController controller)
		{
			this.Controller = controller;
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

		public void BindIdentificationAction(CellClickActionBase identificationAction)
		{
			this.identificationAction = identificationAction;
		}

		public void BindRidingObjectAction(CellClickActionBase ridingObjectAction)
		{
			this.identificationAction = null;
			this.ridingObjectAction = ridingObjectAction;
		}

		public void Steppable()
		{
			if(this.canStep)
			{
				return;
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

		public void Identification()
		{
			if(this.IsIdentification)
			{
				return;
			}

			var adjacentCells = CellManager.Instance.GetAdjacentCellDataLeftTopRightBottom(this.Y, this.X);
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				adjacentCells[i].Steppable();
			}

			this.IsIdentification = true;
			if(this.modifiedIdentificationEvent != null)
			{
				this.modifiedIdentificationEvent(this.IsIdentification);
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

		public bool IsBlank
		{
			get
			{
				return this.identificationAction == null && this.ridingObjectAction == null;
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