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

		public CellData()
		{
			this.IsIdentification = false;
			this.lockCount = 0;
		}

		public abstract void Action();

		public abstract void Description();

		public void AddLock()
		{
			this.lockCount++;
		}

		public void ReleaseLock()
		{
			this.lockCount--;
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