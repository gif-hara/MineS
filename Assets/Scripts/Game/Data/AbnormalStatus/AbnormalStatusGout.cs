using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusGout : AbnormalStatusBase
	{
		public AbnormalStatusGout(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Gout;
			this.OppositeType = GameDefine.AbnormalStatusType.Curing;
		}
	}
}