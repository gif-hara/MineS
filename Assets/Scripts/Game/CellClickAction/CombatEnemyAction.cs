using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CombatEnemyAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			var enemy = EnemyManager.Instance.Enemies[data];
			CombatController.Combat(PlayerManager.Instance.Data, enemy);
			if(enemy.IsDead)
			{
				data.BindCellClickAction(null);
				data.BindDeployDescription(null);
			}
			TurnManager.Instance.Progress(GameDefine.TurnProgressType.EnemyAttack);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Enemy;
			}
		}

		public override Sprite Image
		{
			get
			{
				return null;
			}
		}
	}
}