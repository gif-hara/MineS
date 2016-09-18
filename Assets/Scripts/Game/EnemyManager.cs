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
		private Dictionary<CellData, CharacterData> enemies = new Dictionary<CellData, CharacterData>();

		public CharacterData Create(CellData data)
		{
			Debug.AssertFormat(!this.enemies.ContainsKey(data), "既に敵が存在します.");

			var characterData = new CharacterData();
			characterData.Initialize(10, 0, 10, 10);

			this.enemies.Add(data, characterData);

			return characterData;
		}
	}
}