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
	public abstract class CellData
	{
		public bool IsIdentification{ private set; get; }

		private int lockCount = 0;

		private System.Action<bool> modifiedIdentificationEvent = null;

		private System.Action<int> modifiedLockCountEvent = null;

		public CellData()
		{
			this.IsIdentification = false;
		}

		public virtual void Action()
		{
			this.Identification();
		}

		public abstract void Description();

		public void BindEvent(System.Action<bool> modifiedIdentificationEvent, System.Action<int> modifiedLockCountEvent)
		{
			this.modifiedIdentificationEvent = modifiedIdentificationEvent;
			this.modifiedLockCountEvent = modifiedLockCountEvent;
			this.modifiedIdentificationEvent(this.IsIdentification);
			this.modifiedLockCountEvent(this.lockCount);
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
			this.IsIdentification = true;
			if(this.modifiedIdentificationEvent != null)
			{
				this.modifiedIdentificationEvent(this.IsIdentification);
			}
		}

		public bool IsLock
		{
			get
			{
				return this.lockCount <= 0;
			}
		}
	}
}