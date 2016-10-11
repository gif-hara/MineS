using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusCuring : AbnormalStatusBase
	{
		public AbnormalStatusCuring(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
		}

		public override GameDefine.AbnormalStatusType Type
		{
			get
			{
				return GameDefine.AbnormalStatusType.Curing;
			}
		}

		public override GameDefine.AbnormalStatusType OppositeType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Gout;
			}
		}

		public override GameDefine.AbilityType InvalidateAbilityType
		{
			get
			{
				return GameDefine.AbilityType.None;
			}
		}
	}
}