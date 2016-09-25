using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeAbnormalStatusTrapAction : InvokeTrapActionBase
	{
		private GameDefine.AbnormalStatusType type;

		public InvokeAbnormalStatusTrapAction(GameDefine.AbnormalStatusType type)
		{
			this.type = type;
		}

		public override void Invoke(CellData data)
		{
			data.BindCellClickAction(null);
			this.cellController.SetImage(this.Image);
			PlayerManager.Instance.AddAbnormalStatus(this.type, GameDefine.AbnormalStatusTrapRemainingTurn);
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.trap.poison.Element;
			}
		}
	}
}