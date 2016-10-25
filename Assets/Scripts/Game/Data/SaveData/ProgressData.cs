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
			HK.Framework.SaveData.SetClass<ProgressData>(MineS.SaveData.ProgressKeyName, this);
		}

		public bool IsClearDungeon(GameDefine.DungeonType type)
		{
			return this.clearDungeonFlags.FindIndex(c => c == type) != -1;
		}
	}
}