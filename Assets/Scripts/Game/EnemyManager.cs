using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
	{
		public Dictionary<CellData, EnemyData> Enemies{ private set; get; }

		public Dictionary<EnemyData, CellData> InEnemyCells{ private set; get; }

		protected override void Awake()
		{
			base.Awake();
			this.Enemies = new Dictionary<CellData, EnemyData>();
			this.InEnemyCells = new Dictionary<EnemyData, CellData>();
		}

		void Start()
		{
			TurnManager.Instance.AddEndTurnEvent(this.OnTurnProgress);
			TurnManager.Instance.AddLateEndTurnEvent(this.OnLateTurnProgress);
		}

		public CharacterData Create(CellData cellData)
		{
			Debug.AssertFormat(!this.Enemies.ContainsKey(cellData), "既に敵が存在します.");

			var enemy = DungeonManager.Instance.CreateEnemy();

			this.Enemies.Add(cellData, enemy);
			this.InEnemyCells.Add(enemy, cellData);

			return enemy;
		}

		private void RemoveFromDead()
		{
			var deadEnemies = this.Enemies.Where(e => e.Value.IsDead).Select(e => e.Key).ToList();
			foreach(var k in deadEnemies)
			{
				this.Enemies.Remove(k);
			}
		}

		public int IdentitiedEnemyNumber
		{
			get
			{
				var result = 0;
				foreach(var e in this.Enemies)
				{
					result += e.Key.IsIdentification ? 1 : 0;
				}

				return result;
			}
		}

		public void NextFloor()
		{
			this.Enemies.Clear();
			this.InEnemyCells.Clear();
		}

		public CharacterData GetRandomEnemy(List<CharacterData> ignoreEnemy)
		{
			var target = new List<CharacterData>(this.VisibleEnemies);
			target.RemoveAll(t => ignoreEnemy.Find(i => i == t) != null);

			if(target.Count <= 0)
			{
				return null;
			}

			return target[Random.Range(0, target.Count)];
		}

		private List<CharacterData> VisibleEnemies
		{
			get
			{
				var result = new List<CharacterData>();
				foreach(var e in this.Enemies)
				{
					if(!e.Key.IsIdentification)
					{
						continue;
					}
					result.Add(e.Value);
				}

				return result;
			}
		}

		private void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			foreach(var e in this.Enemies)
			{
				if(e.Value == null || !e.Key.IsIdentification)
				{
					continue;
				}

				e.Value.OnTurnProgress(type, turnCount);
			}

			this.RemoveFromDead();
		}

		private void OnLateTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			foreach(var e in this.Enemies)
			{
				if(e.Value == null || !e.Key.IsIdentification)
				{
					continue;
				}

				e.Key.Controller.CharacterDataObserver.ModifiedData(e.Value);
			}
		}
	}
}