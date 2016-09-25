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
			player.Attack(enemy);

			if(enemy.IsDead)
			{
				PlayerManager.Instance.AddExperience(enemy.Experience);
				PlayerManager.Instance.AddMoney(enemy.Money);
			}
			else
			{
				enemy.Attack(player);
			}

			PlayerManager.Instance.NotifyCharacterDataObservers();
			enemyObserver.ModifiedData(enemy);
		}
	}
}