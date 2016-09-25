﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public static class Calculator
	{
		public static int GetRegenerationValue(int hitPointMax)
		{
			return (hitPointMax / 50) + 1;
		}

		public static int GetPoisonValue(int hitPointMax)
		{
			return (hitPointMax / 50) + 1;
		}

		public static int GetFinalStrength(int baseStrength, List<AbnormalStatusBase> abnormalStatuses)
		{
			float rate = abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Sharpness) != null
				? 2.0f
				: abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Dull) != null
				? 0.5f
				: 1.0f;
			return Mathf.FloorToInt(baseStrength * rate);
		}

		public static int GetFinalDamage(int baseDamage, List<AbnormalStatusBase> abnormalStatuses)
		{
			float rate = abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Gout) != null
				? 2.0f
				: abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Curing) != null
				? 0.5f
				: 1.0f;
			return Mathf.FloorToInt(baseDamage * rate);
		}

		public static int GetFinalExperience(int baseExperience, List<AbnormalStatusBase> abnormalStatuses)
		{
			float rate = abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Happiness) != null
				? 2.0f
				: abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Blur) != null
				? 0.5f
				: 1.0f;

			return Mathf.FloorToInt(baseExperience * rate);
		}

		public static int GetMineTrapDamageValue(int hitPointMax)
		{
			return (hitPointMax / 20);
		}
	}
}