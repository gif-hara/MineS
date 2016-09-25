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
		public override void Invoke(CellData data)
		{
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