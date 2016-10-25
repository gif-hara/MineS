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
	public class SaveData
	{
		public const string ProgressKeyName = "ProgressData";

		public const string WareHouseKeyName = "WareHouseData";

		private static WareHouseData wareHouseData;

		public static ProgressData Progress
		{
			get
			{
				return Get<ProgressData>(ProgressKeyName);
			}
		}

		public static WareHouseData WareHouse
		{
			get
			{
				if(wareHouseData == null)
				{
					wareHouseData = new WareHouseData();
				}

				return wareHouseData;
			}
		}

		private static T Get<T>(string key) where T : class, new()
		{
			var result = HK.Framework.SaveData.GetClass<T>(key, null);
			if(result == null)
			{
				result = new T();
				HK.Framework.SaveData.SetClass<T>(key, result);
			}

			return result;
		}
	}
}