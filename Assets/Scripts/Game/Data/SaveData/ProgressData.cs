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

		[SerializeField]
		private int visitShopCount;

		[SerializeField]
		private int visitBlackSmithCount;

		[SerializeField]
		private int visitTownShopCount;

		[SerializeField]
		private int visitTownBlackSmithCount;

		public List<GameDefine.DungeonType> ClearDungeonFlags{ get { return this.clearDungeonFlags; } }

		public int VisitShopCount{ get { return this.visitShopCount; } }

		public int VisitTownShopCount{ get { return this.visitTownShopCount; } }

		public int VisitBlackSmithCount{ get { return this.visitBlackSmithCount; } }

		public int VisitTownBlackSmithCount{ get { return this.visitTownBlackSmithCount; } }

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

		public void AddVisitShopCount(bool isTown)
		{
			this.visitShopCount++;
			if(isTown)
			{
				this.visitTownShopCount++;
			}
			HK.Framework.SaveData.SetClass<ProgressData>(MineS.SaveData.ProgressKeyName, this);
		}

		public void AddVisitBlackSmithCount(bool isTown)
		{
			this.visitBlackSmithCount++;
			if(isTown)
			{
				this.visitTownBlackSmithCount++;
			}
			HK.Framework.SaveData.SetClass<ProgressData>(MineS.SaveData.ProgressKeyName, this);
		}

		public bool IsClearDungeon(GameDefine.DungeonType type)
		{
			return this.clearDungeonFlags.FindIndex(c => c == type) != -1;
		}
	}
}