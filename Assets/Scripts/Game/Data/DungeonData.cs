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

		public CharacterData CreateEnemy(int floor)
		{
			return this.enemyTable.Create(floor);
		}

		public Item CreateItem()
		{
			return this.itemTable.Create();
		}

		public InvokeTrapActionBase CreateTrap()
		{
			return this.trapTable.Create();
		}
	}
}