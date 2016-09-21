using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusCurse : AbnormalStatus
	{
		public AbnormalStatusCurse(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Curse;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}