using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusHappiness : AbnormalStatusBase
	{
		public AbnormalStatusHappiness(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Happiness;
			this.OppositeType = GameDefine.AbnormalStatusType.Blur;
		}
	}
}