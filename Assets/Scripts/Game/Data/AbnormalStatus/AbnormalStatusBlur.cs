using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusBlur : AbnormalStatus
	{
		public AbnormalStatusBlur(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Blur;
			this.OppositeType = GameDefine.AbnormalStatusType.Happiness;
		}
	}
}