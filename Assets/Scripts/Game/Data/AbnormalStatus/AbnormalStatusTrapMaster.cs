using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusTrapMaster : AbnormalStatus
	{
		public AbnormalStatusTrapMaster(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.TrapMaster;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}