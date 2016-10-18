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

		[SerializeField]
		private DungeonNameFlowController dungeonNameFlowController;

		[SerializeField]
		private List<DungeonDataObserver> observers;

		private int floorCount = 1;

		private UnityEvent nextFloorEvent = new UnityEvent();

		public DungeonData CurrentData{ get { return this.current; } }

		public int Floor{ get { return this.floorCount; } }

		void Start()
		{
			this.dungeonNameFlowController.AddCompleteFadeOutEvent(this.InternalNextFloor);
		}

		public void AddNextFloorEvent(UnityAction otherEvent)
		{
			this.nextFloorEvent.AddListener(otherEvent);
		}

		public void NextFloorEvent(int addValue)
		{
			this.floorCount += addValue;
			this.dungeonNameFlowController.StartFadeOut(this.CurrentData.Name, this.floorCount);
		}

		public EnemyData CreateEnemy(CellController cellController)
		{
			return this.current.CreateEnemy(this.floorCount, cellController);
		}

		public Item CreateItem()
		{
			return this.current.CreateItem();
		}

		public bool CanTurnBack(int addValue)
		{
			return (this.floorCount + addValue) >= 1;
		}

		private void InternalNextFloor()
		{
			EnemyManager.Instance.NextFloor();
			this.nextFloorEvent.Invoke();
			this.observers.ForEach(o => o.ModifiedData(this.CurrentData));
		}
	}
}