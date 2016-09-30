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
		/// <summary>
		/// バフの再生の回復量を返す.
		/// </summary>
		/// <returns>The regeneration value.</returns>
		/// <param name="hitPointMax">Hit point max.</param>
		public static int GetRegenerationValue(int hitPointMax)
		{
			return (hitPointMax / 50) + 1;
		}

		/// <summary>
		/// デバフの毒ダメージ量を返す.
		/// </summary>
		/// <returns>The poison value.</returns>
		/// <param name="hitPointMax">Hit point max.</param>
		public static int GetPoisonValue(int hitPointMax)
		{
			return (hitPointMax / 50) + 1;
		}

		/// <summary>
		/// 最終的な攻撃力を返す.
		/// </summary>
		/// <returns>The final strength.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetFinalStrength(IAttack attacker)
		{
			var baseStrength = attacker.Strength;
			if(attacker.FindAbility(GameDefine.AbilityType.Reinforcement))
			{
				baseStrength += EnemyManager.Instance.IdentitiedEnemyNumber - 1;
			}

			baseStrength += GetArtisanRate(attacker);

			float rate = attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Sharpness)
				? 2.0f
				: attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Dull)
				? 0.5f
				: 1.0f;
			return Mathf.FloorToInt(baseStrength * rate);
		}

		/// <summary>
		/// 最終的なダメージ量を返す.
		/// </summary>
		/// <returns>The final damage.</returns>
		/// <param name="baseDamage">Base damage.</param>
		/// <param name="abnormalStatuses">Abnormal statuses.</param>
		public static int GetFinalDamage(int baseDamage, List<AbnormalStatusBase> abnormalStatuses)
		{
			float rate = abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Gout) != null
				? 2.0f
				: abnormalStatuses.Find(a => a.Type == GameDefine.AbnormalStatusType.Curing) != null
				? 0.5f
				: 1.0f;
			return Mathf.FloorToInt(baseDamage * rate);
		}

		/// <summary>
		/// 最終的に取得する経験値を返す.
		/// </summary>
		/// <returns>The final experience.</returns>
		/// <param name="baseExperience">Base experience.</param>
		/// <param name="attacker">Attacker.</param>
		public static int GetFinalExperience(int baseExperience, IAttack attacker)
		{
			float rate = attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Happiness)
				? 2.0f
				: attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Blur)
				? 0.5f
				: 1.0f;

			return Mathf.FloorToInt(baseExperience * rate * GetProficiencyRate(attacker));
		}

		/// <summary>
		/// 最終的に取得するお金を返す.
		/// </summary>
		/// <returns>The final money.</returns>
		/// <param name="baseMoney">Base money.</param>
		/// <param name="attacker">Attacker.</param>
		public static int GetFinalMoney(int baseMoney, IAttack attacker)
		{
			return Mathf.FloorToInt(baseMoney * GetHoistRate(attacker));
		}

		/// <summary>
		/// 罠の地雷によるダメージ量を返す.
		/// </summary>
		/// <returns>The mine trap damage value.</returns>
		/// <param name="hitPointMax">Hit point max.</param>
		public static int GetMineTrapDamageValue(int hitPointMax)
		{
			return (hitPointMax / 20);
		}

		/// <summary>
		/// 特殊能力の習熟による取得経験値の倍率を返す.
		/// </summary>
		/// <returns>The proficiency rate.</returns>
		/// <param name="attacker">Attacker.</param>
		public static float GetProficiencyRate(IAttack attacker)
		{
			return 1.0f + (float)attacker.GetAbilityNumber(GameDefine.AbilityType.Proficiency) / 10.0f;
		}

		/// <summary>
		/// 特殊能力の巻上による取得出来るお金の倍率を返す.
		/// </summary>
		/// <returns>The hoist rate.</returns>
		/// <param name="attacker">Attacker.</param>
		public static float GetHoistRate(IAttack attacker)
		{
			return 1.0f + (float)attacker.GetAbilityNumber(GameDefine.AbilityType.Hoist) / 5.0f;
		}

		public static int GetArtisanRate(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Artisan);
		}
	}
}