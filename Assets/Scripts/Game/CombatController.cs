using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CombatController
	{
		public static void Combat(CharacterData player, CharacterData enemy)
		{
			CharacterData playerTarget = EnemyManager.Instance.FindProvocationEnemy;
			if(playerTarget == null)
			{
				playerTarget = enemy;
			}

			player.Attack(playerTarget);

			if(CanAttackEnemy(enemy))
			{
				CharacterData enemyTarget = EnemyManager.Instance.AssumptionEnemy;
				enemyTarget = enemyTarget == null ? player : enemyTarget;
				enemy.Attack(enemyTarget);
			}
		}

		/// <summary>
		/// 特殊能力の先手による先制攻撃処理.
		/// </summary>
		/// <param name="player">Player.</param>
		/// <param name="enemy">Enemy.</param>
		public static void CombatInitiative(CharacterData player, CharacterData enemy)
		{
			player.Attack(enemy);
		}

		public static void CombatLongRangeAttack(CharacterData player, CharacterData enemy)
		{
			// 混乱ならなにもしない
			if(enemy.FindAbnormalStatus(GameDefine.AbnormalStatusType.Confusion))
			{
				return;
			}

			CharacterData enemyTarget = EnemyManager.Instance.AssumptionEnemy;
            enemyTarget = enemyTarget == null ? player : enemyTarget;
			enemy.Attack(enemyTarget);
		}

		private static bool CanAttackEnemy(CharacterData enemy)
		{
			return !enemy.IsDead && !enemy.FindAbility(GameDefine.AbilityType.LongRangeAttack);
		}
	}
}