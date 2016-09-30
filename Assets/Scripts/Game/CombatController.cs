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
			player.Attack(enemy);

			if(enemy.IsDead)
			{
				player.Defeat(enemy);
			}
			else if(CanAttackEnemy(enemy))
			{
				enemy.Attack(player);
			}
		}

		public static void CombatLongRangeAttack(CharacterData enemy)
		{
			enemy.Attack(PlayerManager.Instance.Data);
		}

		private static bool CanAttackEnemy(CharacterData enemy)
		{
			return !enemy.FindAbility(GameDefine.AbilityType.LongRangeAttack);
		}
	}
}