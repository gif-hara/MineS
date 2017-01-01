using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AchievementManager : SingletonMonoBehaviour<AchievementManager>
	{
		public AchievementData Data{ private set; get; }

		protected override void Awake()
		{
			this.Initialize();
		}

		void Start()
		{
			DungeonManager.Instance.AddChangeDungeonEvent(this.Initialize);
		}

		public void Initialize()
		{
			this.Data = new AchievementData();
		}

		public void AddDefeatedEnemy(int value)
		{
			this.Data.DefeatedEnemy += value;
		}

		public void AddGiveDamage(int value)
		{
			this.Data.GiveDamage += value;
		}

		public void AddTakeDamage(int value)
		{
			this.Data.TakeDamage += value;
		}

		public void Serialize()
		{
			HK.Framework.SaveData.SetClass<AchievementData>(SerializeKey, this.Data);
		}

		public void Deserialize()
		{
			Assert.IsTrue(HK.Framework.SaveData.ContainsKey(SerializeKey));
			this.Data = HK.Framework.SaveData.GetClass<AchievementData>(SerializeKey, null);
		}

		private static string SerializeKey
		{
			get
			{
				return "AchievementData";
			}
		}
	}
}