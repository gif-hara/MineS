#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEditor;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyList
	{
		private static Dictionary<string, CharacterMasterData> enemies = null;

		public static CharacterMasterData Get(string name)
		{
			if(enemies == null)
			{
				enemies = new Dictionary<string, CharacterMasterData>();
				int i = 0;
				var masterData = AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Enemy/Enemy{0}.asset", i), typeof(CharacterMasterData)) as CharacterMasterData;
				while(masterData != null)
				{
					enemies.Add(masterData.Name, masterData);
					i++;
					masterData = AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Enemy/Enemy{0}.asset", i), typeof(CharacterMasterData)) as CharacterMasterData;
				}
			}

			return enemies[name];
		}
	}
}
#endif