using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SystemManager : MonoBehaviour
	{
		[SerializeField]
		private StringAsset.Finder removeSaveDataMessage0;

		[SerializeField]
		private StringAsset.Finder removeSaveDataMessage1;

		[SerializeField]
		private StringAsset.Finder cancelMessage;

		void Awake()
		{
			QualitySettings.vSyncCount = 1;
			Application.targetFrameRate = 60;
		}

		void Start()
		{
            DungeonManager.Instance.AddNextFloorEvent(this.OnNextFloorEvent);
        }

		void OnApplicationQuit()
		{
			HK.Framework.SaveData.Save();
		}

		void OnApplicationPause(bool pauseStatus)
		{
			if(pauseStatus)
			{
				HK.Framework.SaveData.Save();
			}
		}

		public void ConfirmRemoveSaveData()
		{
			DescriptionManager.Instance.DeployEmergency("ConfirmRemoveSaveData0");
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.removeSaveDataMessage0, () =>
			{
				DescriptionManager.Instance.DeployEmergency("ConfirmRemoveSaveData1");
				confirmManager.Add(this.removeSaveDataMessage1, () =>
				{
					PlayerManager.Instance.RemoveSaveData();
					HK.Framework.SaveData.Clear();
					DungeonManager.Instance.RemoveSaveData();
				}, true);
				confirmManager.Add(this.cancelMessage, () =>
				{
				}, true);
			}, true);
			confirmManager.Add(this.cancelMessage, () =>
			{
			}, true);
		}

		private void OnNextFloorEvent()
		{
            GC.Collect();
        }
	}
}