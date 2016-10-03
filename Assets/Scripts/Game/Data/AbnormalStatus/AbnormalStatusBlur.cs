using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusBlur : AbnormalStatusBase
	{
		public AbnormalStatusBlur(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Blur;
			this.OppositeType = GameDefine.AbnormalStatusType.Happiness;
		}
	}
}