#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEditor;
using System.IO;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SaveDataEditor
	{
		[MenuItem("MineS/SaveData/Complete Progress Data")]
		private static void CompleteProgressData()
		{
			var progressData = ProgressData.Instance;
			progressData.ClearDungeon(GameDefine.DungeonType.ElementaryLevel);
			progressData.ClearDungeon(GameDefine.DungeonType.IntermediateLevel);
			progressData.ClearDungeon(GameDefine.DungeonType.AdvancedLevel);
			progressData.ClearDungeon(GameDefine.DungeonType.CarnageCave);
			progressData.ClearDungeon(GameDefine.DungeonType.TartarusScarletCave);
			progressData.ClearDungeon(GameDefine.DungeonType.BlackSmithEdge);
			progressData.ClearDungeon(GameDefine.DungeonType.PeddlerTemple);
			SaveData.Save();
		}

		[MenuItem("MineS/SaveData/Remove")]
		private static void Remove()
		{
			var saveData = new FileInfo(SaveData.Savedatabase.Path + SaveData.Savedatabase.FileName);
			Debug.LogFormat("file = {0}", saveData.FullName);
			saveData.Delete();
		}
	}
}
#endif