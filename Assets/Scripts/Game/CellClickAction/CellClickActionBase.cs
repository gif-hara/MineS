using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class CellClickActionBase
	{
		protected CellController cellController;

		public abstract GameDefine.EventType EventType{ get; }

		public abstract void Invoke(CellData data);

		public virtual void SetCellData(CellData data)
		{
		}

		public virtual void SetCellController(CellController cellController)
		{
			this.cellController = cellController;
		}

		public virtual void OnUseXray()
		{
		}
	}
}