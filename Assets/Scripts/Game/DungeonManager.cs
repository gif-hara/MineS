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
		private DungeonDataBase current;

		[SerializeField]
		private DungeonNameFlowController dungeonNameFlowController;

		[SerializeField]
		private List<DungeonDataObserver> observers;

		private int floorCount = 1;

		private UnityEvent changeDungeonEvent = new UnityEvent();

		private UnityEvent nextFloorEvent = new UnityEvent();

		public DungeonDataBase CurrentData{ get { return this.current; } }

		public DungeonData CurrentDataAsDungeon{ get { return this.current as DungeonData; } }

		public int Floor{ get { return this.floorCount; } }

		void Start()
		{
			this.dungeonNameFlowController.AddCompleteFadeOutEvent(this.InternalNextFloor);
		}

		public void ChangeDungeonData(DungeonDataBase data, int floor = 1)
		{
			this.current = data;
			this.floorCount = floor;
			this.dungeonNameFlowController.AddCompleteFadeOutEvent(this.InternalChangeDungeon);
			this.NextFloorEvent(0);
		}

		public void AddChangeDungeonEvent(UnityAction otherEvent)
		{
			this.changeDungeonEvent.AddListener(otherEvent);
		}

		public void RemoveChangeDungeonEvent(UnityAction otherEvent)
		{
			this.changeDungeonEvent.RemoveListener(otherEvent);
		}

		public void AddNextFloorEvent(UnityAction otherEvent)
		{
			this.nextFloorEvent.AddListener(otherEvent);
		}

		public void NextFloorEvent(int addValue)
		{
			this.floorCount += addValue;
			//this.InternalNextFloor();
			this.dungeonNameFlowController.StartFadeOut(this.current, this.CurrentData.Name, this.floorCount);
		}

		public EnemyData CreateEnemy(CellController cellController)
		{
			return this.CurrentDataAsDungeon.CreateEnemy(this.floorCount, cellController);
		}

		public Item CreateItem()
		{
			return this.CurrentDataAsDungeon.CreateItem();
		}

		public bool CanTurnBack(int addValue)
		{
			return (this.floorCount + addValue) >= 1;
		}

		public bool IsClear
		{
			get
			{
				return this.floorCount >= this.CurrentDataAsDungeon.FloorMax;
			}
		}

		private void InternalNextFloor()
		{
			EnemyManager.Instance.RemoveAll();
			this.nextFloorEvent.Invoke();
			this.observers.ForEach(o => o.ModifiedData(this.CurrentData));
		}

		private void InternalChangeDungeon()
		{
			this.changeDungeonEvent.Invoke();
			this.dungeonNameFlowController.RemoveCompleteFadeOutEvent(this.InternalChangeDungeon);
		}
			
	}
}