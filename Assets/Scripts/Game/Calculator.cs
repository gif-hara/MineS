using UnityEngine;
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

		public static int GetFinalStrength(IAttack attacker)
		{
			var baseStrength = attacker.Strength;
			if(attacker.FindAbility(GameDefine.AbilityType.Reinforcement))
			{
				baseStrength += EnemyManager.Instance.IdentitiedEnemyNumber - 1;
			}

			float rate = attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Sharpness)
				? 2.0f
				: attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Dull)
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

		public static int GetFinalExperience(int baseExperience, IAttack attacker)
		{
			float rate = attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Happiness)
				? 2.0f
				: attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Blur)
				? 0.5f
				: 1.0f;

			return Mathf.FloorToInt(baseExperience * rate * GetProficiencyRate(attacker));
		}

		public static int GetMineTrapDamageValue(int hitPointMax)
		{
			return (hitPointMax / 20);
		}

		public static float GetProficiencyRate(IAttack attacker)
		{
			return 1.0f + (float)attacker.GetAbilityNumber(GameDefine.AbilityType.Proficiency) / 10.0f;
		}
	}
}