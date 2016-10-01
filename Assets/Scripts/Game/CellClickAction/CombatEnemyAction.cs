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
			CombatController.Combat(PlayerManager.Instance.Data, EnemyManager.Instance.Enemies[data]);
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