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

		private const string EnemyCountKeyName = "EnemyManager_EnemyCount";

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
			Item.AddOnUseItemEvent(this.OnUseItem);
		}

		public EnemyData Create(CellData cellData)
		{
			Debug.AssertFormat(!this.Enemies.ContainsKey(cellData), "既に敵が存在します.");

			var enemy = DungeonManager.Instance.CreateEnemy(cellData.Controller);
			this.Add(cellData, enemy);

			return enemy;
		}

		public EnemyData Create(CellData cellData, CharacterMasterData masterData)
		{
			Debug.AssertFormat(!this.Enemies.ContainsKey(cellData), "既に敵が存在します.");

			var enemy = new EnemyData();
			enemy.Initialize(masterData, cellData.Controller);
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

		public void RemoveAll()
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
			this.RemoveFromDead();
			var visibleEnemies = this.VisibleEnemies;
			foreach(var e in visibleEnemies)
			{
				e.OnTurnProgress(type, turnCount);
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

				e.Value.OnLateTurnProgress(type, turnCount);
				e.Key.Controller.CharacterDataObserver.ModifiedData(e.Value);
			}
		}

		private void OnUseItem(Item item)
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

		public List<CellData> NotIdentifications
		{
			get
			{
				return this.Enemies.Where(e => !e.Key.IsIdentification).Select(e => e.Key).ToList();
			}
		}

		/// <summary>
		/// 挑発持ちの敵を返す.
		/// </summary>
		/// <value>The find provocation enemy.</value>
		public EnemyData FindProvocationEnemy
		{
			get
			{
				var enemies = this.VisibleEnemies.Where(e => e.FindAbility(GameDefine.AbilityType.Provocation)).ToList();

				if(enemies.Count <= 0)
				{
					return null;
				}

				return enemies[Random.Range(0, enemies.Count)];
			}
		}

		public List<EnemyData> VisibleEnemies
		{
			get
			{
				return this.Enemies.Where(e => e.Key.IsIdentification).Select(e => e.Value).ToList();
			}
		}

		public void Serialize()
		{
			HK.Framework.SaveData.SetInt(EnemyCountKeyName, this.Enemies.Count);
			int index = 0;
			foreach(var d in this.Enemies)
			{
				d.Key.Position.Serialize(this.GetCellSerializeKeyName(index));
				d.Value.Serialize(this.GetEnemySerializeKeyName(index));
				index++;
			}
		}

		public void Deserialize()
		{
			if(!HK.Framework.SaveData.ContainsKey(EnemyCountKeyName))
			{
				return;
			}

			this.Enemies = new Dictionary<CellData, EnemyData>();
			this.InEnemyCells = new Dictionary<EnemyData, CellData>();
			var count = HK.Framework.SaveData.GetInt(EnemyCountKeyName);
			for(int i = 0; i < count; i++)
			{
				var cell = Cell.Deserialize(this.GetCellSerializeKeyName(i));
				var enemy = EnemyData.Deserialize(this.GetEnemySerializeKeyName(i), CellManager.Instance.CellControllers[cell.y, cell.x]);
				var cellData = CellManager.Instance.CellDatabase[cell.y, cell.x];
				this.Enemies.Add(cellData, enemy);
				this.InEnemyCells.Add(enemy, cellData);
			}
		}

		private string GetEnemySerializeKeyName(int index)
		{
			return string.Format("EnemyManager_Enemy{0}", index);
		}

		private string GetCellSerializeKeyName(int index)
		{
			return string.Format("EnemyManager_Cell{0}", index);
		}

		public static void BindCombatEnemyAction(CellData cellData, EnemyData enemy)
		{
			cellData.BindCellClickAction(new CombatEnemyAction());
			cellData.BindDeployDescription(new DeployDescriptionOnCharacterData(enemy));
			cellData.Controller.SetCharacterData(enemy);
			cellData.Controller.SetImage(enemy.Image);
			var adjacentCells = cellData.AdjacentCellAll;
			for(int i = 0; i < adjacentCells.Count; i++)
			{
				if(!adjacentCells[i].IsIdentification)
				{
					adjacentCells[i].AddLock();
				}
			}
		}
	}
}