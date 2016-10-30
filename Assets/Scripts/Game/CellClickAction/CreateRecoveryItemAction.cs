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
			data.Controller.SetImage(this.Image);
			data.Controller.SetActiveStatusObject(false);
			data.BindCellClickAction(new InvokeRecoveryItemAction());
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("RecoveryItem"));
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
		}

		public override void SetCellData(CellData data)
		{
			base.SetCellData(data);
			if(data.IsIdentification)
			{
				this.cellController.SetImage(this.Image);
			}
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

		public override void Serialize(int y, int x)
		{
		}

		public override void Deserialize(int y, int x)
		{
		}
	}
}