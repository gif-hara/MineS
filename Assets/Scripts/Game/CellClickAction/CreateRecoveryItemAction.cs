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
			data.Controller.SetImage(DungeonManager.Instance.CurrentData.RecoveryItemImage);
			data.Controller.SetActiveStatusObject(false);
			data.BindCellClickAction(new InvokeRecoveryItemAction());
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("RecoveryItem"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.RecoveryItem;
			}
		}
	}
}