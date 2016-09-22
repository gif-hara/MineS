using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusSpirit : AbnormalStatusBase
	{
		public AbnormalStatusSpirit(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Spirit;
			this.OppositeType = GameDefine.AbnormalStatusType.Headache;
		}
	}
}