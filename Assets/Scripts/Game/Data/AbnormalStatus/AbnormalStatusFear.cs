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
		public AbnormalStatusFear(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Fear;
			this.OppositeType = GameDefine.AbnormalStatusType.Dull;
		}
	}
}