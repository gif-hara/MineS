using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class RecoveryCell : CellData
	{
		private System.Action internalActionEvent = null;

		public RecoveryCell(int y, int x) : base(y, x)
		{
			this.internalActionEvent = this.CreateRecoveryItem;
		}

		protected override void InternalAction()
		{
			if(this.internalActionEvent != null)
			{
				this.internalActionEvent();
			}
		}

		public override void InternalDescription()
		{
			Debug.Log("Recovery Cell Descriptioin");
		}

#region State

		private void CreateRecoveryItem()
		{
			this.controller.SetDebugText("R");
			this.internalActionEvent = this.InvokeRecovery;
		}

		private void InvokeRecovery()
		{
			Debug.Log("Invoke Recovery!");
			this.controller.SetDebugText("");
			this.internalActionEvent = null;
		}

#endregion
	}
}