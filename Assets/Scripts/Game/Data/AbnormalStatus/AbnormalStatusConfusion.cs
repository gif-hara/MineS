using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusConfusion : AbnormalStatusBase
	{
		public AbnormalStatusConfusion(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Confusion;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}