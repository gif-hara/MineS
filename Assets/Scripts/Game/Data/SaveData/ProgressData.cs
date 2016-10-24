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
		public Dictionary<string, bool> ClearDungeonFlags{ private set; get; }

		private const string KeyName = "ProgressData";

		public ProgressData()
		{
			this.ClearDungeonFlags = new Dictionary<string, bool>();
			this.ClearDungeonFlags["Test"] = false;
		}

		public void ClearDungeon(string key)
		{
			if(this.ClearDungeonFlags.ContainsKey(key))
			{
				return;
			}

			this.ClearDungeonFlags.Add(key, true);
		}

		public bool IsClearDungeon(string key)
		{
			return this.ClearDungeonFlags.ContainsKey(key);
		}

		public static ProgressData Instance
		{
			get
			{
				var instance = SaveData.GetClass<ProgressData>(KeyName, null);
				if(instance == null)
				{
					instance = new ProgressData();
					SaveData.SetClass<ProgressData>(KeyName, instance);
				}

				return instance;
			}
		}
	}
}