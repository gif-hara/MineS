using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusCuring : AbnormalStatus
	{
		public AbnormalStatusCuring(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Curing;
			this.OppositeType = GameDefine.AbnormalStatusType.Gout;
		}
	}
}