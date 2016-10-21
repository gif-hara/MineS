using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class VisitBlackSmithAction : CellClickActionBase
	{
		public VisitBlackSmithAction()
		{
		}

		public override void Invoke(CellData data)
		{
			BlackSmithManager.Instance.OpenUI();
		}

		public override void SetCellController(CellController cellController)
		{
			cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
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