using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CloseStairAction : CellClickActionBase
	{
		public CloseStairAction()
		{
		}

		public override void Invoke(CellData data)
		{
			InformationManager.CloseStair();
		}

		public override void SetCellData(CellData data)
		{
			this.cellController.SetImage(this.Image);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Stair;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.closeStairImage.Element;
			}
		}
	}
}