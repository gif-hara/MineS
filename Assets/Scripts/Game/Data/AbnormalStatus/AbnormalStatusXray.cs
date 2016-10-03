using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusXray : AbnormalStatusBase
	{
		public AbnormalStatusXray(int remainingTurn, int waitTurn)
			: base(remainingTurn, waitTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Xray;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}