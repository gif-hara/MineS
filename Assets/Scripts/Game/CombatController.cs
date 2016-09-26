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
		public static void Combat(CharacterData enemy)
		{
			var player = PlayerManager.Instance.Data;
			player.Attack(enemy);

			if(enemy.IsDead)
			{
				PlayerManager.Instance.AddExperience(enemy.Experience);
				PlayerManager.Instance.AddMoney(enemy.Money);
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