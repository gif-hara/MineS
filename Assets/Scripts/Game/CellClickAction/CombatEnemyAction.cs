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
			Debug.Log("Combat!");
		}
	}
}