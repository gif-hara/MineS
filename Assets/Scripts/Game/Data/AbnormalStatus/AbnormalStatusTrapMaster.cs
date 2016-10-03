using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusTrapMaster : AbnormalStatusBase
	{
		public AbnormalStatusTrapMaster(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.TrapMaster;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}