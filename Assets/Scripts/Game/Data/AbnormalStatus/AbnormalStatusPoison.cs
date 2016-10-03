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
		public AbnormalStatusPoison(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Poison;
			this.OppositeType = GameDefine.AbnormalStatusType.Regeneration;
		}

		protected override void InternalTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			var player = PlayerManager.Instance.Data;
			player.TakeDamageRaw(null, Calculator.GetPoisonValue(player.HitPointMax), true);
		}
	}
}