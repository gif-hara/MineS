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
		public static AbnormalStatusBase Create(GameDefine.AbnormalStatusType type, int remainingTurn)
		{
			switch(type)
			{
			case GameDefine.AbnormalStatusType.Regeneration:
				return new AbnormalStatusRegeneration(remainingTurn);
			case GameDefine.AbnormalStatusType.Sharpness:
				return new AbnormalStatusSharpness(remainingTurn);
			case GameDefine.AbnormalStatusType.Curing:
				return new AbnormalStatusCuring(remainingTurn);
			case GameDefine.AbnormalStatusType.Xray:
				return new AbnormalStatusXray(remainingTurn);
			case GameDefine.AbnormalStatusType.TrapMaster:
				return new AbnormalStatusTrapMaster(remainingTurn);
			case GameDefine.AbnormalStatusType.Happiness:
				return new AbnormalStatusHappiness(remainingTurn);
			case GameDefine.AbnormalStatusType.Poison:
				return new AbnormalStatusPoison(remainingTurn);
			case GameDefine.AbnormalStatusType.Blur:
				return new AbnormalStatusBlur(remainingTurn);
			case GameDefine.AbnormalStatusType.Gout:
				return new AbnormalStatusGout(remainingTurn);
			case GameDefine.AbnormalStatusType.Dull:
				return new AbnormalStatusDull(remainingTurn);
			case GameDefine.AbnormalStatusType.Fear:
				return new AbnormalStatusFear(remainingTurn);
			case GameDefine.AbnormalStatusType.Seal:
				return new AbnormalStatusSeal(remainingTurn);
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return null;
			}
		}
	}
}