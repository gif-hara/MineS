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

		public InvokeAbnormalStatusTrapAction(GameDefine.TrapType type)
		{
			this.type = GameDefine.ConvertTrapTypeToAbnormalStatusType(type);
		}

		public override void InternalInvoke(CellData data)
		{
			data.BindCellClickAction(null);
			this.cellController.SetImage(this.Image);
			PlayerManager.Instance.AddAbnormalStatus(this.type, GameDefine.AbnormalStatusTrapRemainingTurn);
		}

		public override Sprite Image
		{
			get
			{
				switch(this.type)
				{
				case GameDefine.AbnormalStatusType.Poison:
					return TextureManager.Instance.trap.poison.Element;
				case GameDefine.AbnormalStatusType.Blur:
					return TextureManager.Instance.trap.blur.Element;
				case GameDefine.AbnormalStatusType.Dull:
					return TextureManager.Instance.trap.dull.Element;
				case GameDefine.AbnormalStatusType.Gout:
					return TextureManager.Instance.trap.gout.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. type = {0}", type);
					return null;
				}
			}
		}
	}
}