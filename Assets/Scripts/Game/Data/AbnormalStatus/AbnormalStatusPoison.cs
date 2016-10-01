using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusPoison : AbnormalStatusBase
	{
		public AbnormalStatusPoison(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Poison;
			this.OppositeType = GameDefine.AbnormalStatusType.Regeneration;
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			base.OnTurnProgress(type, turnCount);
			var player = PlayerManager.Instance.Data;
			player.TakeDamageRaw(null, Calculator.GetPoisonValue(player.HitPointMax), true);
		}
	}
}