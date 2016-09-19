using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
	{
		[SerializeField]
		private List<CharacterDataObserver> observers;

		[SerializeField]
		private ExperienceData experienceData;

		[SerializeField]
		private CharacterMasterData growthData;

		public PlayerData Data{ private set; get; }

		public ExperienceData ExperienceData{ get { return this.experienceData; } }

		void Start()
		{
			this.Data = new PlayerData();
			this.NotifyObservers();
		}

		public void Recovery(int value)
		{
			this.Data.Recovery(value);
			this.NotifyObservers();
		}

		public void AddExperience(int value)
		{
			this.Data.AddExperience(value);
			while(this.Data.CanLevelUp)
			{
				this.Data.LevelUp(this.growthData);
				this.NotifyObservers();
			}
		}

		public void NotifyObservers()
		{
			for(int i = 0; i < this.observers.Count; i++)
			{
				this.observers[i].ModifiedData(this.Data);
			}
		}
	}
}