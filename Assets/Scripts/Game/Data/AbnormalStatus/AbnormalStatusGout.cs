using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusGout : AbnormalStatus
	{
		public AbnormalStatusGout(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Gout;
			this.OppositeType = GameDefine.AbnormalStatusType.Curing;
		}
	}
}