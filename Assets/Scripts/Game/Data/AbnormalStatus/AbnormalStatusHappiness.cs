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
		public AbnormalStatusHappiness(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Happiness;
			this.OppositeType = GameDefine.AbnormalStatusType.Blur;
		}
	}
}