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

		void Start()
		{
			this.Enemies = new Dictionary<CellData, CharacterData>();
			DungeonManager.Instance.AddNextFloorEvent(this.NextFloor);
		}

		public CharacterData Create(CellData data)
		{
			Debug.AssertFormat(!this.Enemies.ContainsKey(data), "既に敵が存在します.");

			var characterData = new CharacterData();
			characterData.Initialize(10, 0, 10, 10);

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
	}
}