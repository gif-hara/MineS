using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusHeadache : AbnormalStatus
	{
		public AbnormalStatusHeadache(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Headache;
			this.OppositeType = GameDefine.AbnormalStatusType.Spirit;
		}
	}
}