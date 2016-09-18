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
			var enemyData = EnemyManager.Instance.Enemies[data];
			CombatController.Combat(enemyData, data.Controller.CharacterDataObserver);

			if(enemyData.IsDead)
			{
				data.BindCellClickAction(null);
				data.Controller.SetDebugText("");
				data.Controller.SetActiveStatusObject(false);
				EnemyManager.Instance.Remove(data);
				var adjacentCells = data.AdjacentCellAll;
				for(int i = 0; i < adjacentCells.Count; i++)
				{
					if(adjacentCells[i].IsLock)
					{
						adjacentCells[i].ReleaseLock();
					}
				}
			}
		}
	}
}