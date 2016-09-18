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
		public static void Combat(CharacterData enemy, CharacterDataObserver enemyObserver)
		{
			var player = PlayerManager.Instance.Data;
			enemy.TakeDamage(player.Strength, false);

			if(!enemy.IsDead)
			{
				player.TakeDamage(enemy.Strength, false);
			}

			PlayerManager.Instance.NotifyObservers();
			enemyObserver.ModifiedData(enemy);
		}
	}
}