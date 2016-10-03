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
		public AbnormalStatusCuring(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Curing;
			this.OppositeType = GameDefine.AbnormalStatusType.Gout;
		}
	}
}