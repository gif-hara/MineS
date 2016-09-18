using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateStairAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetDebugText("F");
			data.BindRidingObjectAction(new InvokeNextFloorAction());
		}
	}
}