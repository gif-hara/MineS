using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateRecoveryItemAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetDebugText("R");
			data.BindRidingObjectAction(new InvokeRecoveryAction());
		}
	}
}