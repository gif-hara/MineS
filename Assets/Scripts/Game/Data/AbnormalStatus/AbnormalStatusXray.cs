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
		public AbnormalStatusXray(int remainingTurn)
			: base(remainingTurn)
		{
			this.Type = GameDefine.AbnormalStatusType.Xray;
			this.OppositeType = GameDefine.AbnormalStatusType.None;
		}
	}
}