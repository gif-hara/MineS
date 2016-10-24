using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SaveDataEditor
	{
#if UNITY_EDITOR
		[MenuItem("MineS/SaveData/Complete Progress Data")]
		#endif
		public static void CompleteProgressData()
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

#if UNITY_EDITOR
		[MenuItem("MineS/SaveData/Remove")]
		#endif
		public static void Remove()
		{
			var saveData = new FileInfo(SaveData.Savedatabase.Path + SaveData.Savedatabase.FileName);
			saveData.Delete();
		}
	}
}
