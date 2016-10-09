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
	public class EnemyManager : SingletonMonoBehaviour<EnemyManager>, ITurnProgress
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

		public EnemyData Create(CellData cellData)
		{
			Debug.AssertFormat(!this.Enemies.ContainsKey(cellData), "既に敵が存在します.");

			var enemy = DungeonManager.Instance.CreateEnemy();
			this.Add(cellData, enemy);

			return enemy;
		}

		public void Add(CellData cellData, EnemyData enemy)
		{
			this.Enemies.Add(cellData, enemy);
			this.InEnemyCells.Add(enemy, cellData);
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
				return this.Enemies.Count(e => e.Key.IsIdentification);
			}
		}

		public void NextFloor()
		{
			this.Enemies.Clear();
			this.InEnemyCells.Clear();
		}

		public CharacterData GetRandomEnemy(List<CharacterData> ignoreEnemy)
		{
			var target = this.VisibleEnemies.Where(e => ignoreEnemy.Find(i => i == e) == null).ToList();

			if(target.Count <= 0)
			{
				return null;
			}

			return target[Random.Range(0, target.Count)];
		}

		public void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			var visibleEnemies = this.VisibleEnemies;
			foreach(var e in visibleEnemies)
			{
				e.OnTurnProgress(type, turnCount);
			}

			this.RemoveFromDead();
		}

		public List<CellData> NotIdentifications
		{
			get
			{
				return this.Enemies.Where(e => !e.Key.IsIdentification).Select(e => e.Key).ToList();
			}
		}

		private List<EnemyData> VisibleEnemies
		{
			get
			{
				return this.Enemies.Where(e => e.Key.IsIdentification).Select(e => e.Value).ToList();
			}
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