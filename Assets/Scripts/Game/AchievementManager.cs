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
		public int DefeatedEnemy{ private set; get; }

		public int GiveDamage{ private set; get; }

		public int TakeDamage{ private set; get; }

		void Start()
		{
			DungeonManager.Instance.AddChangeDungeonEvent(this.Initialize);
		}

		public void Initialize()
		{
			this.DefeatedEnemy = 0;
			this.GiveDamage = 0;
			this.TakeDamage = 0;
		}

		public void AddDefeatedEnemy(int value)
		{
			this.DefeatedEnemy += value;
		}

		public void AddGiveDamage(int value)
		{
			this.GiveDamage += value;
		}

		public void AddTakeDamage(int value)
		{
			this.TakeDamage += value;
		}
	}
}