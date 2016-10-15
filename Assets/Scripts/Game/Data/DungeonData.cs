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
	public class DungeonData : ScriptableObject
	{
		[SerializeField]
		private StringAsset.Finder dungeonName;

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
		private ShopTable shopTable;

		[SerializeField]
		private Range createRecoveryItemRange;

		[SerializeField]
		private Range createEnemyRange;

		[SerializeField]
		private Range createItemRange;

		[SerializeField]
		private Range createTrapRange;

		public string Name{ get { return this.dungeonName.ToString(); } }

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

		public bool CanCreateShop(int floor)
		{
			return this.shopTable.CanCreate(floor);
		}

		public Inventory CreateShopInventory(int floor)
		{
			return this.shopTable.CreateInventory(floor);
		}
	}
}