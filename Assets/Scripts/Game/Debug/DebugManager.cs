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
	public class DebugManager : SingletonMonoBehaviour<DebugManager>
	{
		public class ModifiedEventInt : UnityEvent<int>
		{
			
		}

		public int AbnormalStatusRemainingTurn{ get { return this.abnormalStatusRemainingTurn; } }

		[SerializeField]
		private int abnormalStatusRemainingTurn;

		[SerializeField]
		private TalkChunkData testTalk;

		private ModifiedEventInt abnormalStatusRemainingTurnEvent = new ModifiedEventInt();

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				CellManager.Instance.DebugAction();
			}
			if(Input.GetKeyDown(KeyCode.W))
			{
				PlayerManager.Instance.DebugRecoveryHitPoint();
			}
			if(Input.GetKeyDown(KeyCode.E))
			{
				PlayerManager.Instance.DebugRecoveryHitPointDying();
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				PlayerManager.Instance.DebugRecoveryArmor();
			}
			if(Input.GetKeyDown(KeyCode.T))
			{
				PlayerManager.Instance.DebugZeroArmor();
			}
			if(Input.GetKeyDown(KeyCode.A))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Regeneration);
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Sharpness);
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Curing);
			}
			if(Input.GetKeyDown(KeyCode.F))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.TrapMaster);
			}
			if(Input.GetKeyDown(KeyCode.G))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Xray);
			}
			if(Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Happiness);
			}
			if(Input.GetKeyDown(KeyCode.Z))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Poison);
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Blur);
			}
			if(Input.GetKeyDown(KeyCode.C))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Gout);
			}
			if(Input.GetKeyDown(KeyCode.V))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Dull);
			}
			if(Input.GetKeyDown(KeyCode.B))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Confusion);
			}
			if(Input.GetKeyDown(KeyCode.Z))
			{
				InformationManager.AddMessage("一行表示テスト");
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				InformationManager.AddMessage("二行表示テスト\n二行表示テスト");
			}
			if(Input.GetKeyDown(KeyCode.C))
			{
				TalkManager.Instance.StartTalk(this.testTalk, null);
			}
			if(Input.GetKeyDown(KeyCode.M))
			{
				UnityAdsController.Instance.ShowRewardedAd();
			}
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				HK.Framework.SaveData.Save();
			}
		}

		public void AddAbnormalStatusRemainingTurn(int value)
		{
			this.abnormalStatusRemainingTurn += value;
			this.abnormalStatusRemainingTurnEvent.Invoke(this.abnormalStatusRemainingTurn);
		}

		public void AddAbnormalStatusRemainingTurnEvent(UnityAction<int> func)
		{
			func.Invoke(this.abnormalStatusRemainingTurn);
			this.abnormalStatusRemainingTurnEvent.AddListener(func);
		}

		public void AllDungeonClear()
		{
			SaveDataEditor.CompleteProgressData();
		}

		public void RemoveSaveData()
		{
			SaveDataEditor.Remove();
		}
	}
}