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
        [SerializeField]
        private AchievementData data;
        public AchievementData Data
		{
            get { return this.data; }
        }

		protected override void Awake()
		{
            this.Initialize();
        }

		void Start()
		{
			DungeonManager.Instance.AddImmediateChangeDungeonEvent(this.Initialize);
		}

		void Update()
		{
            this.UpdatePlayTime();
        }

		public void Initialize()
		{
			this.data = new AchievementData();
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
			HK.Framework.SaveData.SetClass<AchievementData>(SerializeKey, this.data);
		}

		public void Deserialize()
		{
			Assert.IsTrue(HK.Framework.SaveData.ContainsKey(SerializeKey));
			this.data = HK.Framework.SaveData.GetClass<AchievementData>(SerializeKey, null);
		}

		private void UpdatePlayTime()
		{
			if(DungeonManager.Instance.CurrentDataAsDungeon == null || ResultManager.Instance.IsResult)
			{
                return;
            }

            this.data.PlayTimer += Time.deltaTime;
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