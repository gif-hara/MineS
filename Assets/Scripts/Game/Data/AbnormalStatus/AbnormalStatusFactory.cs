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
		public static AbnormalStatusBase Create(GameDefine.AbnormalStatusType type, IAttack holder, int remainingTurn, int waitTurn)
		{
			switch(type)
			{
			case GameDefine.AbnormalStatusType.Regeneration:
				return new AbnormalStatusRegeneration(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Sharpness:
				return new AbnormalStatusSharpness(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Curing:
				return new AbnormalStatusCuring(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Xray:
				return new AbnormalStatusXray(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.TrapMaster:
				return new AbnormalStatusTrapMaster(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Happiness:
				return new AbnormalStatusHappiness(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Poison:
				return new AbnormalStatusPoison(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Blur:
				return new AbnormalStatusBlur(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Gout:
				return new AbnormalStatusGout(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Dull:
				return new AbnormalStatusDull(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Fear:
				return new AbnormalStatusFear(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Seal:
				return new AbnormalStatusSeal(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Confusion:
				return new AbnormalStatusConfusion(holder, remainingTurn, waitTurn);
			case GameDefine.AbnormalStatusType.Assumption:
				return new AbnormalStatusAssumption(holder, remainingTurn, waitTurn);
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return null;
			}
		}
	}
}