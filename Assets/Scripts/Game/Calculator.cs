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
		private static bool canInvokeSummon = true;

		/// <summary>
		/// バフの再生の回復量を返す.
		/// </summary>
		/// <returns>The regeneration value.</returns>
		/// <param name="user">User.</param>
		/// <param name="hitPointMax">Hit point max.</param>
		public static int GetRegenerationValue(IAttack user, int hitPointMax)
		{
			var rate = user.CharacterType == GameDefine.CharacterType.Player
				? 50
				: 5;
			var result = (hitPointMax / rate);
			result = result <= 0 ? 1 : result;
			return result;
		}

		/// <summary>
		/// 特殊能力の自己再生の回復量を返す.
		/// </summary>
		/// <returns>The regeneration ability value.</returns>
		/// <param name="hitPointMax">Hit point max.</param>
		public static int GetRegenerationAbilityValue(IAttack user)
		{
			return user.GetAbilityNumber(GameDefine.AbilityType.Regeneration) * 2;
		}

		/// <summary>
		/// 特殊能力の慈悲の回復量を返す.
		/// </summary>
		/// <returns>The mercy ability value.</returns>
		/// <param name="user">User.</param>
		public static int GetMercyAbilityValue(IAttack user)
		{
			return (user.HitPointMax / 10);
		}

		/// <summary>
		/// デバフの毒ダメージ量を返す.
		/// </summary>
		/// <returns>The poison value.</returns>
		/// <param name="holder">Holder.</param>
		/// <param name="hitPointMax">Hit point max.</param>
		public static int GetPoisonValue(IAttack holder, int hitPointMax)
		{
			var rate = holder.CharacterType == GameDefine.CharacterType.Player
				? 50
				: 10;
			var result = (hitPointMax / rate);
			result = result <= 0 ? 1 : result;
			return result;
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
				var reinforcementPower = attacker.GetAbilityNumber(GameDefine.AbilityType.Reinforcement) * (EnemyManager.Instance.IdentitiedEnemyNumber - 1) * 2;
				reinforcementPower = reinforcementPower < 0 ? 0 : reinforcementPower;
				baseStrength += reinforcementPower;
			}

			if(attacker.FindAbility(GameDefine.AbilityType.Artisan))
			{
				baseStrength += GetArtisanRate(attacker);
			}

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
		/// <param name="receiver">Receiver.</param>
		public static int GetFinalDamage(int baseDamage, IAttack receiver)
		{
			float rate = receiver.FindAbnormalStatus(GameDefine.AbnormalStatusType.Gout)
				? 2.0f
				: receiver.FindAbnormalStatus(GameDefine.AbnormalStatusType.Curing)
				? 0.5f
				: 1.0f;
			int result = Mathf.FloorToInt(baseDamage * rate) - (receiver.GetAbilityNumber(GameDefine.AbilityType.Protection) * 3);

			// 敵のみ一致団結の処理を行う.
			if(receiver.CharacterType == GameDefine.CharacterType.Enemy)
			{
				result -= (EnemyManager.Instance.VisibleEnemies.Count - 1) * (EnemyManager.Instance.GetAbilityUnityNumber * 2);
			}
			result = result < 1 ? 1 : result;

			return result;
		}

		/// <summary>
		/// 最終的に取得する経験値を返す.
		/// </summary>
		/// <returns>The final experience.</returns>
		/// <param name="baseExperience">Base experience.</param>
		/// <param name="attacker">Attacker.</param>
		public static int GetFinalExperience(int baseExperience, IAttack attacker)
		{
			float rate = 1.0f;
			rate *= attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Happiness) || CellManager.Instance.FindStoneStatue(GameDefine.StoneStatueType.Happiness)
				? 2.0f : 1.0f;
			rate /= attacker.FindAbnormalStatus(GameDefine.AbnormalStatusType.Blur)
				? 2.0f : 1.0f;

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
			if(!attacker.FindAbility(GameDefine.AbilityType.Artisan))
			{
				return 0;
			}
			return 2 + attacker.GetAbilityNumber(GameDefine.AbilityType.Artisan) * 4;
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

			return (abilityNumber * 5) + 2;
		}

		/// <summary>
		/// 特殊能力の五右衛門によるお金の獲得量を返す.
		/// </summary>
		/// <returns>The goemon value.</returns>
		/// <param name="damage">Damage.</param>
		/// <param name="attacker">Attacker.</param>
		public static int GetGoemonValue(IAttack attacker)
		{
			return Random.Range(10, (attacker.GetAbilityNumber(GameDefine.AbilityType.Goemon) * 100) + 1);
		}

		/// <summary>
		/// 特殊能力の窃盗が成功したか返す.
		/// </summary>
		/// <returns><c>true</c> if is success theft the specified attacker; otherwise, <c>false</c>.</returns>
		/// <param name="attacker">Attacker.</param>
		public static bool IsSuccessTheft(IAttack attacker)
		{
			var probability = attacker.GetAbilityNumber(GameDefine.AbilityType.Theft) + 2;

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
			if(CellManager.Instance.FindStoneStatue(GameDefine.StoneStatueType.Souvenir))
			{
				return true;
			}
				
			if(attacker != null)
			{
				probability += attacker.GetAbilityNumber(GameDefine.AbilityType.Fortune);
			}
			return probability > Random.Range(0, 100);
		}

		/// <summary>
		/// 特殊能力の捨身による攻撃力上昇値を返す.
		/// </summary>
		/// <returns>The risk of life add strength value.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetRiskOfLifeAddStrengthValue(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.RiskOfLife) * 10;
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
		/// 特殊能力の減退によるターン数減少値を返す.
		/// </summary>
		/// <returns>The weal sub turn.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetWeakSubTurn(IAttack attacker)
		{
			return -attacker.GetAbilityNumber(GameDefine.AbilityType.Weak) * 2;
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

		/// <summary>
		/// 特殊能力の伝染によるターン数上昇値を返す.
		/// </summary>
		/// <returns>The infection add turn.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetInfectionAddTurn(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Infection) * 2;
		}

		/// <summary>
		/// 特殊能力の精巧によるアーマー上昇値を返す.
		/// </summary>
		/// <returns>The exquisite armor value.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetExquisiteArmorValue(IAttack attacker)
		{
		    if (!attacker.FindAbility(GameDefine.AbilityType.Exquisite))
		    {
		        return 0;
		    }

			return 7 + attacker.GetAbilityNumber(GameDefine.AbilityType.Exquisite) * 8;
		}

		/// <summary>
		/// 回復系アイテムの回復量を返す.
		/// </summary>
		/// <returns>The usable item recovery value.</returns>
		/// <param name="baseValue">Base value.</param>
		/// <param name="user">User.</param>
		public static int GetUsableItemRecoveryValue(int baseValue, IAttack user)
		{
			if(user.FindAbility(GameDefine.AbilityType.HealingBuddha))
			{
				float rate = 0.10f + user.GetAbilityNumber(GameDefine.AbilityType.HealingBuddha) * 0.15f;
				baseValue += Mathf.FloorToInt(baseValue * rate);
			}

			return baseValue;
		}

		/// <summary>
		/// 特殊能力による状態異常付与が行えるか返す.
		/// </summary>
		/// <returns><c>true</c> if can add abnormal status from ability the specified abilityType attacker; otherwise, <c>false</c>.</returns>
		/// <param name="abilityType">Ability type.</param>
		/// <param name="attacker">Attacker.</param>
		public static bool CanAddAbnormalStatusFromAbility(GameDefine.AbilityType abilityType, IAttack attacker)
		{
			if(!attacker.FindAbility(abilityType))
			{
				return false;
			}

		    var rate = attacker.CharacterType == GameDefine.CharacterType.Player ? 2 : 10;
			var probability = 10 + attacker.GetAbilityNumber(abilityType) * rate;

			return probability > Random.Range(0, 100);
		}

		/// <summary>
		/// 特殊能力の回復による回復値を返す.
		/// </summary>
		/// <returns>The ability recovery value.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetAbilityRecoveryValue(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Recovery) * 3;
		}

		/// <summary>
		/// 合成に必要なお金を返す.
		/// </summary>
		/// <returns>The synthesis need money.</returns>
		/// <param name="baseEquipment">Base equipment.</param>
		/// <param name="targetEquipment">Target equipment.</param>
		public static int GetSynthesisNeedMoney(Item baseEquipment, Item targetEquipment)
		{
			return (baseEquipment.InstanceData.PurchasePrice + targetEquipment.InstanceData.PurchasePrice) / 2;
		}

		/// <summary>
		/// 消印に必要なお金を返す.
		/// </summary>
		/// <returns>The remove ability need money.</returns>
		/// <param name="item">Item.</param>
		public static int GetRemoveAbilityNeedMoney(Item item)
		{
			return item.InstanceData.SellingPrice;
		}

		/// <summary>
		/// 特殊能力の先手のダメージを返す.
		/// </summary>
		/// <returns>The initiative damage.</returns>
		/// <param name="attacker">Attacker.</param>
		public static int GetInitiativeDamage(IAttack attacker)
		{
			return attacker.GetAbilityNumber(GameDefine.AbilityType.Initiative) * 9;
		}

		/// <summary>
		/// 特殊能力の跳返のダメージを返す.
		/// </summary>
		/// <returns>The splash damage.</returns>
		/// <param name="attacker">Attacker.</param>
		/// <param name="takeDamage">Take damage.</param>
		public static int GetSplashDamage(IAttack attacker, int takeDamage)
		{
			var rate = 22 - attacker.GetAbilityNumber(GameDefine.AbilityType.Splash) * 2;
			rate = rate < 10 ? 10 : rate;
			return Mathf.Max(takeDamage / rate, 1);
		}

		/// <summary>
		/// 特殊能力の召喚の成功確率を返す.
		/// </summary>
		/// <value><c>true</c> if can invoke summon; otherwise, <c>false</c>.</value>
		public static bool CanInvokeSummon
		{
			get
			{
				if(!canInvokeSummon)
				{
					return false;
				}

				canInvokeSummon = false;
				return Random.value < 0.2f;
			}
		}

		/// <summary>
		/// 特殊能力の召喚を使用可能にする.
		/// </summary>
		public static void ResetCanInvokeSummon()
		{
			canInvokeSummon = true;
		}

		/// <summary>
		/// 投擲物のダメージを返す.
		/// </summary>
		/// <returns>The throwing item damage.</returns>
		/// <param name="item">Item.</param>
		public static int GetThrowingItemDamage(ThrowingInstanceData item)
		{
			var playerData = PlayerManager.Instance.Data;
			return item.Power + Mathf.FloorToInt((float)playerData.BaseStrength * ((float)item.PlayerPower / 100));
		}

		/// <summary>
		/// 湧き水の石像のポーション生成確率を返す.
		/// </summary>
		/// <value><c>true</c> if can create stone statue spring water; otherwise, <c>false</c>.</value>
		public static bool CanCreateStoneStatueSpringWater
		{
			get
			{
				return 0.25f > Random.value;
			}
		}

	    /// <summary>
	    /// 特殊能力の吸収の回復量を返す
	    /// </summary>
	    /// <returns></returns>
	    public static int GetAbsorptionRecovery(IAttack attacker, int giveDamage)
	    {
	        var rate = attacker.CharacterType == GameDefine.CharacterType.Player ? 3 : 2;
	        return giveDamage / rate;
	    }

	}
}