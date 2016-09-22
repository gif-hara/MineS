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
			TurnManager.Instance.AddEvent(this.OnTurnProgress);
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
				e.Key.Controller.CharacterDataObserver.ModifiedData(e.Value);
			}
		}
	}
}