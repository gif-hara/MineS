﻿using UnityEngine;
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
		private DungeonDataBase tutorialData;

		[SerializeField]
		private DungeonDataBase townData;

		[SerializeField]
		private DungeonNameFlowController dungeonNameFlowController;

		[SerializeField]
		private List<DungeonDataObserver> observers;

		[SerializeField]
		private TitleFlowController titleFlowController;

		[SerializeField]
		private StringAsset.Finder giveupMessage;

		[SerializeField]
		private StringAsset.Finder giveupCancelMessage;

		private int floorCount = 1;

		private UnityEvent preChangeDungeonEvent = new UnityEvent();

		private UnityEvent changeDungeonEvent = new UnityEvent();

		private UnityEvent nextFloorEvent = new UnityEvent();

		public DungeonDataBase CurrentData{ get { return this.current; } }

		public DungeonData CurrentDataAsDungeon{ get { return this.current as DungeonData; } }

		public int Floor{ get { return this.floorCount; } }

		private int cachedAddFloorCount = 0;

		protected override void Awake()
		{
			base.Awake();
			if(DungeonSerializer.ContainsDungeonData)
			{
				var serializeData = DungeonSerializer.DeserializeDungeonData();
				this.floorCount = serializeData.floor;
				this.current = serializeData.dungeonData;
			}
		}

		void Start()
		{
			this.dungeonNameFlowController.AddCompleteFadeOutEvent(this.InternalNextFloor);
			this.dungeonNameFlowController.AddStartTextFadeIn(this.PlayBGM);
			this.dungeonNameFlowController.AddCompleteFadeInEvent(this.InvokeOtherProccess);
			this.titleFlowController.AddStartFadeOutEvent(this.OnStartTitleFadeOut);

			if(DungeonSerializer.ContainsDungeonData)
			{
				this.Deserialize();
			}
			else
			{
				CellManager.Instance.CreateCellDatabaseFromDungeonData();
			}

			this.observers.ForEach(o => o.ModifiedData(this.current));
		}

		void OnApplicationQuit()
		{
			if(this.current.Serializable && !ResultManager.Instance.IsResult)
			{
				this.Serialize();
			}
			else
			{
				DungeonSerializer.InvalidSaveData();
			}
		}

		void OnApplicationPause(bool pauseStatus)
		{
			if(!pauseStatus)
			{
				return;
			}

			OnApplicationQuit();
		}

		public void ChangeDungeonData(DungeonDataBase data, bool immediateFadeOut, int floor = 1)
		{
			this.current = data;
			this.floorCount = floor;
			this.dungeonNameFlowController.AddCompleteFadeOutEvent(this.InternalChangeDungeon);
			this.NextFloorEvent(0, immediateFadeOut);
		}

		public void AddPreChangeDungeonEvent(UnityAction otherEvent)
		{
			this.preChangeDungeonEvent.AddListener(otherEvent);
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

		public void NextFloorEvent(int addValue, bool immediateFadeOut)
		{
			this.cachedAddFloorCount = addValue;
			SEManager.Instance.PlaySE(SEManager.Instance.stair);
			if(this.current.CanPlayBGM(this.floorCount + addValue))
			{
				BGMManager.Instance.FadeOut();
			}
			//this.InternalNextFloor();
			this.dungeonNameFlowController.StartFadeOut(this.current, this.CurrentData.Name, this.floorCount + addValue, immediateFadeOut);
		}

		public EnemyData CreateEnemy(CellController cellController)
		{
			return this.CurrentDataAsDungeon.CreateEnemy(this.floorCount, cellController);
		}

		public Item CreateItem()
		{
			return this.CurrentDataAsDungeon.CreateItem();
		}

		public void ClearDungeon(GameDefine.GameResultType type)
		{
			if(this.CurrentDataAsDungeon != null && type == GameDefine.GameResultType.Clear)
			{
				this.CurrentDataAsDungeon.ClearDungeon();
			}
			PlayerManager.Instance.CloseInventoryUI();
			HK.Framework.SaveData.Save();
		}

		public void ConfirmGiveup()
		{
			DescriptionManager.Instance.DeployEmergency("ConfirmGiveup");
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.giveupMessage, () =>
			{
				PlayerManager.Instance.Giveup();
				OptionManager.Instance.CloseUI();
				this.ChangeDungeonData(this.townData, false);
			}, true);
			confirmManager.Add(this.giveupCancelMessage, () =>
			{

			}, true);
		}

		public void RemoveSaveData()
		{
			this.ChangeDungeonData(this.tutorialData, false);
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
			this.floorCount += this.cachedAddFloorCount;
			EnemyManager.Instance.RemoveAll();
			OptionManager.Instance.CloseUI();
			this.nextFloorEvent.Invoke();
			this.observers.ForEach(o => o.ModifiedData(this.CurrentData));
		}

		private void InternalChangeDungeon()
		{
			this.preChangeDungeonEvent.Invoke();
			this.changeDungeonEvent.Invoke();
			this.dungeonNameFlowController.RemoveCompleteFadeOutEvent(this.InternalChangeDungeon);
		}

		private void PlayBGM()
		{
			if(!this.current.CanPlayBGM(this.floorCount))
			{
				return;
			}

			BGMManager.Instance.StartBGM(this.current.GetBGM(this.floorCount));
		}

		private void OnStartTitleFadeOut()
		{
			if(!MineS.SaveData.Progress.IsCompleteTutorial)
			{
				this.ChangeDungeonData(this.tutorialData, true);
			}
			else
			{
				BGMManager.Instance.StartBGM(this.current.GetBGM(this.floorCount));
			}
		}

		private void InvokeOtherProccess()
		{
			this.current.InvokeOtherProccess();
		}

		private void Serialize()
		{
			PlayerManager.Instance.Serialize();
			CellManager.Instance.Serialize();
			EnemyManager.Instance.Serialize();
			ItemManager.Instance.Serialize();
			DungeonSerializer.Save(this.floorCount, this.current);
			HK.Framework.SaveData.Save();
		}

		private void Deserialize()
		{
			CellManager.Instance.Deserialize();
			EnemyManager.Instance.Deserialize();
			ItemManager.Instance.Deserialize();
			DungeonSerializer.InvalidSaveData();

			CellManager.Instance.LateDeserialize();

			HK.Framework.SaveData.Save();
		}
	}
}