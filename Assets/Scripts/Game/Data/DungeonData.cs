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
		private Sprite stairImage;

		[SerializeField]
		private Sprite recoveryItemImage;

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
		private Range createEnemyRange;

		[SerializeField]
		private Range createItemRange;

		[SerializeField]
		private Range createTrapRange;

		public Sprite StairImage{ get { return this.stairImage; } }

		public Sprite RecoveryItemImage{ get { return this.recoveryItemImage; } }

		public Range CreateRecoveryItemRange{ get { return this.createRecoveryItemRange; } }

		public Range CreateEnemyRange{ get { return this.createEnemyRange; } }

		public Range CreateItemRange{ get { return this.createItemRange; } }

		public Range CreateTrapRange{ get { return this.createTrapRange; } }

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