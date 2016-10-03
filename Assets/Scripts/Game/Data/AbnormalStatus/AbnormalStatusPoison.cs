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
		public AbnormalStatusPoison(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Poison;
			this.OppositeType = GameDefine.AbnormalStatusType.Regeneration;
		}

		protected override void InternalTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.Holder.TakeDamageRaw(null, Calculator.GetPoisonValue(this.Holder.HitPointMax), true);
		}
	}
}