using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DungeonManager : SingletonMonoBehaviour<DungeonManager>
	{
		[SerializeField]
		private DungeonData current;

		private int floorCount = 1;

		private UnityEvent nextFloorEvent = new UnityEvent();

		public void AddNextFloorEvent(UnityAction otherEvent)
		{
			this.nextFloorEvent.AddListener(otherEvent);
		}

		public void NextFloorEvent()
		{
			this.floorCount++;
			this.nextFloorEvent.Invoke();
		}

		public CharacterData CreateEnemy()
		{
			return this.current.CreateEnemy(this.floorCount);
		}
	}
}