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
		public CreateStairAction()
		{
			this.EventType = GameDefine.EventType.Stair;
		}

		public override void Invoke(CellData data)
		{
			data.Controller.SetDebugText("F");
			data.BindCellClickAction(new InvokeNextFloorAction());
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("NextFloor"));
		}
	}
}