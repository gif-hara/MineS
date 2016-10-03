using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusTrapMaster : AbnormalStatusBase
	{
		public AbnormalStatusTrapMaster(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.TrapMaster;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}