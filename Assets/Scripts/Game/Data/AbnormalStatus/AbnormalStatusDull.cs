using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusDull : AbnormalStatusBase
	{
		public AbnormalStatusDull(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Dull;
			this.OppositeType = GameDefine.AbnormalStatusType.Sharpness;
		}
	}
}