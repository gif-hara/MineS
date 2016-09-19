using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
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
				return floor >= this.floorMin && floor <= this.floorMax;
			}

			public CharacterData Create()
			{
				var result = new CharacterData();
				var index = GameDefine.Lottery(this.enemies);
				result.Initialize(this.enemies[index].MasterData);
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

		[System.Serializable]
		private class EnemyCreateTable : IProbability
		{
			[SerializeField]
			private CharacterMasterData masterData;

			[SerializeField]
			private int probability;

			public CharacterMasterData MasterData{ get { return this.masterData; } }

			public int Probability{ get { return this.probability; } }
		}

	}
}