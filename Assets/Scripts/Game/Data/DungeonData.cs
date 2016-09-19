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
		private EnemyTable enemyTable;

		[SerializeField]
		private Range createRecoveryItemRange;

		[SerializeField]
		private Range createEnemyRange;

		public string Name{ get { return this.dungeonName.ToString(); } }

		public Range CreateRecoveryItemRange{ get { return this.createRecoveryItemRange; } }

		public Range CreateEnemyRange{ get { return this.createEnemyRange; } }

		public CharacterData CreateEnemy(int floor)
		{
			return this.enemyTable.Create(floor);
		}
	}
}