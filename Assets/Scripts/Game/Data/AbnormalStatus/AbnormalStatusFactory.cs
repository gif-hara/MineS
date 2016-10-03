using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusFactory
	{
		public static AbnormalStatusBase Create(GameDefine.AbnormalStatusType type, int remainingTurn, int waitTurn)
		{
			switch(type)
			{
			case GameDefine.AbnormalStatusType.Regeneration:
				return new AbnormalStatusRegeneration(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Sharpness:
				return new AbnormalStatusSharpness(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Curing:
				return new AbnormalStatusCuring(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Xray:
				return new AbnormalStatusXray(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.TrapMaster:
				return new AbnormalStatusTrapMaster(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Happiness:
				return new AbnormalStatusHappiness(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Poison:
				return new AbnormalStatusPoison(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Blur:
				return new AbnormalStatusBlur(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Gout:
				return new AbnormalStatusGout(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Dull:
				return new AbnormalStatusDull(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Fear:
				return new AbnormalStatusFear(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Seal:
				return new AbnormalStatusSeal(remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Confusion:
				return new AbnormalStatusConfusion(remainingTurn, waitTurn);
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return null;
			}
		}
	}
}