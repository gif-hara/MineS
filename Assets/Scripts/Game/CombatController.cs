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

			if(CanAttackEnemy(enemy))
			{
				enemy.Attack(player);
			}
		}

		public static void CombatLongRangeAttack(CharacterData player, CharacterData enemy)
		{
			enemy.Attack(player);
		}

		private static bool CanAttackEnemy(CharacterData enemy)
		{
			return !enemy.IsDead && !enemy.FindAbility(GameDefine.AbilityType.LongRangeAttack);
		}
	}
}