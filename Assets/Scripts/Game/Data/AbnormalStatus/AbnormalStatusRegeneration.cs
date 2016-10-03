using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusRegeneration : AbnormalStatusBase
	{
		public AbnormalStatusRegeneration(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Regeneration;
			this.OppositeType = GameDefine.AbnormalStatusType.Poison;
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			base.OnTurnProgress(type, turnCount);
			var playerManager = PlayerManager.Instance;
			playerManager.RecoveryHitPoint(Calculator.GetRegenerationValue(playerManager.Data.HitPointMax), true);
		}
	}
}