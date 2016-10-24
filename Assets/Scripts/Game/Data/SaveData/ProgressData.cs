using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class ProgressData
	{
		[SerializeField]
		private List<GameDefine.DungeonType> clearDungeonFlags;

		private const string KeyName = "ProgressData";

		private static ProgressData instance = null;

		public List<GameDefine.DungeonType> ClearDungeonFlags{ get { return this.clearDungeonFlags; } }

		public ProgressData()
		{
			this.clearDungeonFlags = new List<GameDefine.DungeonType>();
		}

		public void ClearDungeon(GameDefine.DungeonType type)
		{
			if(this.clearDungeonFlags.FindIndex(c => c == type) != -1)
			{
				return;
			}

			this.clearDungeonFlags.Add(type);
			SaveData.SetClass<ProgressData>(KeyName, this);
		}

		public bool IsClearDungeon(GameDefine.DungeonType type)
		{
			return this.clearDungeonFlags.FindIndex(c => c == type) != -1;
		}

		public static ProgressData Instance
		{
			get
			{
				if(instance == null)
				{
					instance = SaveData.GetClass<ProgressData>(KeyName, null);
					if(instance == null)
					{
						instance = new ProgressData();
						SaveData.SetClass<ProgressData>(KeyName, instance);
					}
				}

				return instance;
			}
		}
	}
}