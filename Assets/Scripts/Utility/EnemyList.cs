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
		private static List<CharacterMasterData> enemies = null;

		public static CharacterMasterData Get(string name)
		{
			if(enemies == null)
			{
				enemies = new List<CharacterMasterData>();
				int i = 0;
				var masterData = AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Enemy/Enemy{0}.asset", i), typeof(CharacterMasterData)) as CharacterMasterData;
				while(masterData != null)
				{
					enemies.Add(masterData);
					i++;
					masterData = AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Enemy/Enemy{0}.asset", i), typeof(CharacterMasterData)) as CharacterMasterData;
				}
			}

			var levelString = "Lv.";
			var fixedName = name.Substring(0, name.IndexOf(levelString));
			var level = int.Parse(name.Substring(name.IndexOf(levelString) + levelString.Length));
			return enemies.Find(e => e.Name == fixedName && e.Level == level);
		}
	}
}
#endif