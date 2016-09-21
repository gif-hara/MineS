using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusDull : AbnormalStatus
	{
		public AbnormalStatusDull(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Dull;
			this.OppositeType = GameDefine.AbnormalStatusType.Sharpness;
		}
	}
}