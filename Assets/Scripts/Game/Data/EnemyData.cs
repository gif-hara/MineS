using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyData : CharacterData
	{
		public override void Dead(CharacterData attacker)
		{
			var cellData = EnemyManager.Instance.InEnemyCells[this];
			cellData.BindCellClickAction(null);
			cellData.Controller.SetActiveStatusObject(false);
			cellData.Controller.SetImage(null);
			EnemyManager.Instance.Remove(cellData);
			var adjacentCells = cellData.AdjacentCellAll;
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