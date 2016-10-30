using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DungeonSerializer
	{
		private const string SaveDungeonKeyName = "SaveDungeon";

		public static bool ContainsDungeonData
		{
			get
			{
				return HK.Framework.SaveData.ContainsKey(SaveDungeonKeyName);
			}
		}

		public static void Save(int floor, DungeonDataBase dungeonData)
		{
			var serializeData = new DungeonSerializeData(floor, dungeonData);
			HK.Framework.SaveData.SetClass<DungeonSerializeData>(SaveDungeonKeyName, serializeData);
		}

		public static void InvalidSaveData()
		{
			HK.Framework.SaveData.Remove(SaveDungeonKeyName);
		}

		public static void SerializeCellData(CellData[,] cellDatabase, int rowMax, int culumnMax)
		{
			for(int y = 0; y < rowMax; y++)
			{
				for(int x = 0; x < culumnMax; x++)
				{
					cellDatabase[y, x].Serialize();
				}
			}
		}

		public static CellData[,] DeserializeCellData(CellController[,] cellControllers, int rowMax, int culumnMax)
		{
			var result = new CellData[rowMax, culumnMax];
			for(int y = 0; y < rowMax; y++)
			{
				for(int x = 0; x < culumnMax; x++)
				{
					var cellData = new CellData(y, x, -1, cellControllers[y, x]);
					cellData.Deserialize();
					result[y, x] = cellData;
				}
			}

			return result;
		}

		public static DungeonSerializeData DeserializeDungeonData()
		{
			return HK.Framework.SaveData.GetClass<DungeonSerializeData>(SaveDungeonKeyName, null);
		}

		private static string GetCellDataKeyName(int y, int x)
		{
			return string.Format("CellData_{0}_{1}", y, x);
		}
	}
}