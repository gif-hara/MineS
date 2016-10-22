using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class DungeonData : DungeonDataBase
	{
		[SerializeField]
		private int floorMax;

		[SerializeField]
		private EnemyTable enemyTable;

		[SerializeField]
		private ItemTable itemTable;

		[SerializeField]
		private TrapTable trapTable;

		[SerializeField]
		private BlackSmithTable blackSmithTable;

		[SerializeField]
		private Range createRecoveryItemRange;

		[SerializeField]
		private Range createAnvilRange;

		[SerializeField]
		private Range createMoneyRange;

		[SerializeField]
		private Range createEnemyRange;

		[SerializeField]
		private Range createItemRange;

		[SerializeField]
		private Range createTrapRange;

		[SerializeField]
		private Range acquireMoneyRange;

		public Range CreateRecoveryItemRange{ get { return this.createRecoveryItemRange; } }

		public Range CreateAnvilRange{ get { return this.createAnvilRange; } }

		public Range CreateMoneyRange{ get { return this.createMoneyRange; } }

		public Range CreateEnemyRange{ get { return this.createEnemyRange; } }

		public Range CreateItemRange{ get { return this.createItemRange; } }

		public Range CreateTrapRange{ get { return this.createTrapRange; } }

		public Range AcquireMoneyRange{ get { return this.acquireMoneyRange; } }

		[ContextMenu("Check")]
		private void AssertionCheck()
		{
			this.enemyTable.Check(this.floorMax);
		}

		public override CellData[,] Create(CellManager cellManager)
		{
			return new DungeonCreator().Create(cellManager, this, GameDefine.CellRowMax, GameDefine.CellCulumnMax);
		}

		public EnemyData CreateEnemy(int floor, CellController cellController)
		{
			return this.enemyTable.Create(floor, cellController);
		}

		public Item CreateItem()
		{
			return this.itemTable.Create();
		}

		public InvokeTrapActionBase CreateTrap()
		{
			return this.trapTable.Create();
		}

		public bool CanCreateBlackSmith(int floor)
		{
			return this.blackSmithTable.CanCreate(floor);
		}
	}
}