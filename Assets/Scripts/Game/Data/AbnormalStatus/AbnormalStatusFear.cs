using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusFear : AbnormalStatusBase
	{
		public AbnormalStatusFear(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Fear;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}