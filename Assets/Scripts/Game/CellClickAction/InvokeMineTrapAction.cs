using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeMineTrapAction : InvokeTrapActionBase
	{
		public override void InternalInvoke(CellData data)
		{
			data.BindCellClickAction(null);
			data.BindDeployDescription(null);
			this.cellController.SetImage(this.Image);
			var playerManager = PlayerManager.Instance;
			playerManager.TakeDamageRaw(Calculator.GetMineTrapDamageValue(playerManager.Data.HitPointMax), true);
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.trap.mine.Element;
			}
		}
	}
}