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

		private CharacterMasterData masterData = null;

		private const string EnemySerializeFormat = "CreateEnemyActionEnemy_{0}_{1}";

		public CreateEnemyAction()
		{
			this.masterData = null;
		}

		/// <summary>
		/// .ctor
		/// 固定の敵を生成可能.
		/// </summary>
		/// <param name="masterData">Master data.</param>
		public CreateEnemyAction(CharacterMasterData masterData)
		{
			this.masterData = masterData;
		}

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
			if(this.enemy != null)
			{
				return;
			}

			if(this.masterData == null)
			{
				this.enemy = EnemyManager.Instance.Create(data);
			}
			else
			{
				this.enemy = EnemyManager.Instance.Create(data, this.masterData);
			}
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

		public override void Serialize(int y, int x)
		{
			this.enemy.Serialize(this.GetEnemySerializeName(y, x));
		}

		public override void Deserialize(int y, int x)
		{
			this.enemy = EnemyData.Deserialize(this.GetEnemySerializeName(y, x), CellManager.Instance.CellControllers[y, x]);
		}

		private string GetEnemySerializeName(int y, int x)
		{
			return string.Format(EnemySerializeFormat, y, x);
		}
	}
}