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
		private EnemyData enemy;

		public override void Invoke(CellData data)
		{
			InformationManager.OnVisibleEnemy(this.enemy);
			this.enemy.OnVisible(data);
			PlayerManager.Instance.Data.OnInitiative(this.enemy);
		}

		public override void OnIdentification(CellData cellData)
		{
			this.enemy.OnIdentification(cellData);
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