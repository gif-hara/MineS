using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusSharpness : AbnormalStatusBase
	{
		public AbnormalStatusSharpness(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Sharpness;
			this.OppositeType = GameDefine.AbnormalStatusType.Dull;
		}
	}
}