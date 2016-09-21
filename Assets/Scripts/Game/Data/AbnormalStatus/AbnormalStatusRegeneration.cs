using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusRegeneration : AbnormalStatus
	{
		public AbnormalStatusRegeneration(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Regeneration;
			this.OppositeType = GameDefine.AbnormalStatusType.Poison;
		}
	}
}