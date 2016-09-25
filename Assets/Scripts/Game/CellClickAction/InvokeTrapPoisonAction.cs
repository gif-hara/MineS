using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeTrapPoisonAction : InvokeTrapActionBase
	{
		public override void Invoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Poison, 5);
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