using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateBlackSmithAction : CellClickActionBase
	{
		public CreateBlackSmithAction()
		{
		}

		public override void Invoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			data.BindCellClickAction(new VisitBlackSmithAction(false));
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("BlackSmith"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.BlackSmith;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.blackSmith.Element;
			}
		}
	}
}