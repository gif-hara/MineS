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
			var player = PlayerManager.Instance.Data;
			player.TakeDamageRaw(null, Calculator.GetMineTrapDamageValue(player.HitPointMax), true);
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.trap.mine.Element;
			}
		}

		public override string DescriptionKey
		{
			get
			{
				return "TrapMine";
			}
		}
	}
}