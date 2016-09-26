using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
	{
		public Dictionary<CellData, CharacterData> Enemies{ private set; get; }

		protected override void Awake()
		{
			base.Awake();
			this.Enemies = new Dictionary<CellData, CharacterData>();
		}

		void Start()
		{
			DungeonManager.Instance.AddNextFloorEvent(this.NextFloor);
			TurnManager.Instance.AddEndTurnEvent(this.OnTurnProgress);
			TurnManager.Instance.AddLateEndTurnEvent(this.OnLateTurnProgress);
		}

		public CharacterData Create(CellData data)
		{
			Debug.AssertFormat(!this.Enemies.ContainsKey(data), "既に敵が存在します.");

			var characterData = DungeonManager.Instance.CreateEnemy();

			this.Enemies.Add(data, characterData);

			return characterData;
		}

		public void Remove(CellData data)
		{
			this.Enemies.Remove(data);
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

		private void NextFloor()
		{
			this.Enemies.Clear();
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