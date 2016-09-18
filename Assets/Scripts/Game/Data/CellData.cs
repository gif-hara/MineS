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
		private int x;

		private int y;

		private bool canStep = false;

		private bool isIdentification = false;

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
			this.x = x;
			this.y = y;
		}

		public void Action()
		{
			var actionableType = this.GetActionableType;
			if(actionableType != GameDefine.ActionableType.OK && this.infeasibleEvent != null)
			{
				this.infeasibleEvent(actionableType);
				return;
			}

			if(!this.isIdentification)
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
			this.modifiedIdentificationEvent(this.isIdentification);
			this.modifiedLockCountEvent(this.lockCount);
		}

		public void BindIdentificationAction(CellClickActionBase identificationAction)
		{
			this.identificationAction = identificationAction;
		}

		public void BindRidingObjectAction(CellClickActionBase ridingObjectAction)
		{
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
			if(this.isIdentification)
			{
				return;
			}

			var adjacentCells = CellManager.Instance.GetAdjacentCellDataLeftTopRightBottom(this.y, this.x);
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				adjacentCells[i].Steppable();
			}

			this.isIdentification = true;
			if(this.modifiedIdentificationEvent != null)
			{
				this.modifiedIdentificationEvent(this.isIdentification);
			}
		}

		public bool IsLock
		{
			get
			{
				return this.lockCount > 0;
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