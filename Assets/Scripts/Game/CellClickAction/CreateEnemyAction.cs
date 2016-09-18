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
			data.Controller.SetDebugText("E");
			var enemy = EnemyManager.Instance.Create(data);
			data.BindRidingObjectAction(new CombatEnemyAction());
			data.Controller.SetStatus(enemy);
		}
	}
}