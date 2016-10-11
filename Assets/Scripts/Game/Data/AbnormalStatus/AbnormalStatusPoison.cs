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
		public AbnormalStatusPoison(IAttack holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
		}

		public override GameDefine.AbnormalStatusType Type
		{
			get
			{
				return GameDefine.AbnormalStatusType.Poison;
			}
		}

		public override GameDefine.AbnormalStatusType OppositeType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Regeneration;
			}
		}

		public override GameDefine.AbilityType InvalidateAbilityType
		{
			get
			{
				return GameDefine.AbilityType.Serum;
			}
		}

		protected override void InternalTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.Holder.TakeDamageRaw(null, Calculator.GetPoisonValue(this.Holder.HitPointMax), true);
		}
	}
}