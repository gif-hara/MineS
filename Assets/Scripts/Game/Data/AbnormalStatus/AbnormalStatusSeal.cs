using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusSeal : AbnormalStatusBase
	{
		public AbnormalStatusSeal(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Seal;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}