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
			data.Controller.SetImage(TextureManager.Instance.recoveryItem.Element);
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

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.recoveryItem.Element;
			}
		}
	}
}