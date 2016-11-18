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
		private CellData cellData;

		public override void Invoke(CellData data)
		{
			var enemy = this.Enemy;
			CombatController.Combat(PlayerManager.Instance.Data, enemy);
			TurnManager.Instance.Progress(GameDefine.TurnProgressType.EnemyAttack);

			if(enemy.IsDead)
			{
				this.cellData.BindDeployDescription(null);
			}
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
		}

		public override void SetCellData(CellData data)
		{
			this.cellData = data;
			this.cellController.SetImage(this.Image);
			this.cellController.SetCharacterData(EnemyManager.Instance.Enemies[this.cellData]);
			this.cellData.BindDeployDescription(new DeployDescriptionOnCharacterData(this.Enemy));
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
				return this.Enemy.Image;
			}
		}

		private EnemyData Enemy
		{
			get
			{
				return EnemyManager.Instance.Enemies[this.cellData];
			}
		}

		public override void Serialize(int y, int x)
		{
		}

		public override void Deserialize(int y, int x)
		{
		}
	}
}