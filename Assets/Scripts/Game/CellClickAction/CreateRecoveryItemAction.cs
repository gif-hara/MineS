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
		public CreateRecoveryItemAction()
		{
			this.EventType = GameDefine.EventType.RecoveryItem;
		}

		public override void Invoke(CellData data)
		{
			data.Controller.SetDebugText("R");
			data.Controller.SetActiveStatusObject(false);
			data.BindCellClickAction(new InvokeRecoveryAction());
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("RecoveryItem"));
		}
	}
}