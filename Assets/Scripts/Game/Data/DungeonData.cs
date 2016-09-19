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
		[System.Serializable]
		public class EnemyTable
		{
			[System.Serializable]
			public class Element
			{
				[SerializeField]
				private int floorMin;

				[SerializeField]
				private int floorMax;

				[SerializeField]
				private List<EnemyCreateTable> enemies;

				public bool IsMatchFloor(int floor)
				{
					return floor >= this.floorMin && floor <= this.floorMin;
				}

				public CharacterData Create()
				{
					var result = new CharacterData();
					int probabilityMax = 0;
					for(int i = 0; i < this.enemies.Count; i++)
					{
						probabilityMax += this.enemies[i].Probability;
					}

					int probability = Random.Range(0, probabilityMax);
					int currentProbability = 0;
					for(int i = 0; i < this.enemies.Count; i++)
					{
						var table = this.enemies[i];
						if(probability >= currentProbability && probability < (currentProbability + table.Probability))
						{
							result.Initialize(table.MasterData);
							return result;
						}
					}

					Debug.AssertFormat(false, "計算を間違えている可能性があります.");
					return result;
				}
			}

			[SerializeField]
			private List<Element> elements;

			public CharacterData Create(int floor)
			{
#if DEBUG
				Debug.AssertFormat(this.elements.FindAll(e => e.IsMatchFloor(floor)).Count == 1, "敵テーブルが無い、または複数ありました.");
#endif
				return this.elements.Find(e => e.IsMatchFloor(floor)).Create();
			}
		}

		[System.Serializable]
		private class EnemyCreateTable
		{
			[SerializeField]
			private CharacterMasterData masterData;

			[SerializeField]
			private int probability;

			public CharacterMasterData MasterData{ get { return this.masterData; } }

			public int Probability{ get { return this.probability; } }
		}

		[SerializeField]
		private StringAsset.Finder dungeonName;

		[SerializeField]
		private EnemyTable enemyTable;

		public string Name{ get { return this.dungeonName.ToString(); } }

		public CharacterData CreateEnemy(int floor)
		{
			return this.enemyTable.Create(floor);
		}
	}
}