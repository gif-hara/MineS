using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateEnemyAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			var enemy = EnemyManager.Instance.Create(data);
			data.BindCellClickAction(new CombatEnemyAction());
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("Enemy"));
			data.Controller.SetStatus(enemy);
            data.Controller.SetImage(enemy.Image);
			var adjacentCells = data.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(!adjacentCells[i].IsIdentification)
				{
					adjacentCells[i].AddLock();
				}
			}
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Enemy;
			}
		}
	}
}