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

		public PlayerData Data{ private set; get; }

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

		public void NotifyObservers()
		{
			for(int i = 0; i < this.observers.Count; i++)
			{
				this.observers[i].ModifiedData(this.Data);
			}
		}
	}
}