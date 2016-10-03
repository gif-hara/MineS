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

			if(attacker.FindAbility(GameDefine.AbilityType.RiskOfLife) && attacker.Armor > 0)
			{
				baseStrength += GetRiskOfLifeAddStrengthValue(attacker);
			}

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

		/// <summary>
		/// 特殊能力の匠による攻撃力上昇値を返す.
		/// </summary>
		/// <returns>The artisan rate.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetArtisanRate(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Artisan);
		}

		/// <summary>
		/// 特殊能力の改修によるアーマー回復量を返す.
		/// </summary>
		/// <returns>The repair value.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetRepairValue(IAttack attacker)
		{
			var abilityNumber = attacker.GetAbilityNumber(GameDefine.AbilityType.Repair);
			if(abilityNumber <= 0)
			{
				return 0;
			}

			return (abilityNumber * 2) + 1;
		}

		/// <summary>
		/// 特殊能力の五右衛門によるお金の獲得量を返す.
		/// </summary>
		/// <returns>The goemon value.</returns>
		/// <param name="damage">Damage.</param>
		/// <param name="attacker">Attacker.</param>
		public static int GetGoemonValue(int damage, IAttack attacker)
		{
			var abilityNumber = attacker.GetAbilityNumber(GameDefine.AbilityType.Goemon);
			if(abilityNumber <= 0)
			{
				return 0;
			}

			return Mathf.FloorToInt(damage * ((float)abilityNumber / 10.0f)) + 1;
		}

		/// <summary>
		/// 特殊能力の窃盗が成功したか返す.
		/// </summary>
		/// <returns><c>true</c> if is success theft the specified attacker; otherwise, <c>false</c>.</returns>
		/// <param name="attacker">Attacker.</param>
		public static bool IsSuccessTheft(IAttack attacker)
		{
			var probability = attacker.GetAbilityNumber(GameDefine.AbilityType.Theft) + 3;

			return probability > Random.Range(0, 100);
		}

		/// <summary>
		/// アイテムをドロップ可能か返す.
		/// </summary>
		/// <returns><c>true</c> if can drop item the specified probability attacker; otherwise, <c>false</c>.</returns>
		/// <param name="probability">Probability.</param>
		/// <param name="attacker">Attacker.</param>
		public static bool CanDropItem(int probability, IAttack attacker)
		{
			probability += attacker.GetAbilityNumber(GameDefine.AbilityType.Fortune);
			return probability > Random.Range(0, 100);
		}

		/// <summary>
		/// 特殊能力の捨身による攻撃力上昇値を返す.
		/// </summary>
		/// <returns>The risk of life add strength value.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetRiskOfLifeAddStrengthValue(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.RiskOfLife) * 2;
		}

		/// <summary>
		/// 特殊能力の捨身によるアーマー減少値を返す.
		/// </summary>
		/// <returns>The risk of life sub armor value.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetRiskOfLifeSubArmorValue(IAttack attacker)
		{
			return (attacker.GetAbilityNumber(GameDefine.AbilityType.RiskOfLife) / 2) + 1;
		}

		/// <summary>
		/// 特殊能力の増進によるターン数上昇値を返す.
		/// </summary>
		/// <returns>The enhancement add turn.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetEnhancementAddTurn(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Enhancement) * 2;
		}

		/// <summary>
		/// 特殊能力の免疫によるターン数減少値を返す.
		/// </summary>
		/// <returns>The immunity sub turn.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetImmunitySubTurn(IAttack attacker)
		{
			return -attacker.GetAbilityNumber(GameDefine.AbilityType.Immunity) * 2;
		}

		public static int GetExquisiteArmorValue(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Exquisite);
		}
	}
}