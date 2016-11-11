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
		public AbnormalStatusRegeneration()
			: base()
		{
		}

		public AbnormalStatusRegeneration(IAttack holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
		}

		public override GameDefine.AbnormalStatusType Type
		{
			get
			{
				return GameDefine.AbnormalStatusType.Regeneration;
			}
		}

		public override GameDefine.AbnormalStatusType OppositeType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Poison;
			}
		}

		public override GameDefine.AbilityType InvalidateAbilityType
		{
			get
			{
				return GameDefine.AbilityType.None;
			}
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			if(this.Holder == null)
			{
				return;
			}
			base.OnTurnProgress(type, turnCount);
			this.Holder.RecoveryHitPoint(Calculator.GetRegenerationValue(this.Holder, this.Holder.HitPointMax), true);
		}
	}
}