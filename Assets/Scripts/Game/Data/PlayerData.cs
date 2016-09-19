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
	public class PlayerData : CharacterData
	{
		public int Level{ private set; get; }

		public PlayerData()
		{
			this.Initialize("", 100, 100, 4, 0, 0);
			this.Level = 1;
		}

		public void AddExperience(int value)
		{
			this.Experience += value;
		}

		public void LevelUp(CharacterMasterData growthData)
		{
			this.Level++;

			this.HitPointMax += growthData.HitPoint;
			if(this.HitPoint <= this.HitPointMax)
			{
				this.HitPoint = this.HitPointMax;
			}

			this.MagicPointMax += growthData.MagicPoint;
			if(this.MagicPoint <= this.MagicPointMax)
			{
				this.MagicPoint = this.MagicPointMax;
			}

			this.Strength += growthData.Strength;
			this.Armor += growthData.Armor;
		}

		public bool CanLevelUp
		{
			get
			{
				return this.Experience >= PlayerManager.Instance.ExperienceData.NeedNextLevel(this.Level);
			}
		}
	}
}