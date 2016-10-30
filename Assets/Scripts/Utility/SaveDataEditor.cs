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
			var progressData = MineS.SaveData.Progress;
			progressData.ClearDungeon(GameDefine.DungeonType.ElementaryLevel);
			progressData.ClearDungeon(GameDefine.DungeonType.IntermediateLevel);
			progressData.ClearDungeon(GameDefine.DungeonType.AdvancedLevel);
			progressData.ClearDungeon(GameDefine.DungeonType.CarnageCave);
			progressData.ClearDungeon(GameDefine.DungeonType.TartarusScarletCave);
			progressData.ClearDungeon(GameDefine.DungeonType.BlackSmithEdge);
			progressData.ClearDungeon(GameDefine.DungeonType.PeddlerTemple);
			progressData.AddVisitShopCount(true);
			progressData.AddVisitBlackSmithCount(true);
			HK.Framework.SaveData.Save();
		}

#if UNITY_EDITOR
		[MenuItem("MineS/SaveData/Remove")]
		#endif
		public static void Remove()
		{
			HK.Framework.SaveData.Clear();
			HK.Framework.SaveData.Save();
		}
	}
}
