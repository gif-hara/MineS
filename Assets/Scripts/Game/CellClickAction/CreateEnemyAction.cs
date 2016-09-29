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
		private CharacterData enemy;

		public override void Invoke(CellData data)
		{
			data.BindCellClickAction(new CombatEnemyAction());
			data.BindDeployDescription(new DeployDescriptionOnCharacterData(this.enemy));
			data.Controller.SetStatus(this.enemy);
			data.Controller.SetImage(this.enemy.Image);
			var adjacentCells = data.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(!adjacentCells[i].IsIdentification)
				{
					adjacentCells[i].AddLock();
				}
			}
		}

		public override void SetCellData(CellData data)
		{
			this.enemy = EnemyManager.Instance.Create(data);
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
				return this.enemy.Image;
			}
		}
	}
}