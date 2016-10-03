using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusSeal : AbnormalStatusBase
	{
		public AbnormalStatusSeal(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Seal;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}