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

#if UNITY_EDITOR
		void Update()
		{
			if(UnityEngine.Input.GetKeyDown(KeyCode.Q))
			{
				CellManager.Instance.DebugAction();
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.W))
			{
				PlayerManager.Instance.DebugRecoveryHitPoint();
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.E))
			{
				PlayerManager.Instance.DebugRecoveryHitPointDying();
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.R))
			{
				PlayerManager.Instance.DebugRecoveryArmor();
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.T))
			{
				PlayerManager.Instance.DebugZeroArmor();
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.A))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Regeneration);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.S))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Sharpness);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.D))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Curing);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.F))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.TrapMaster);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.G))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Xray);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Happiness);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.Z))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Poison);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.X))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Blur);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.C))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Gout);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.V))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Dull);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.B))
			{
				PlayerManager.Instance.DebugAddAbnormalStatus((int)GameDefine.AbnormalStatusType.Confusion);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.Z))
			{
				InformationManager.AddMessage("一行表示テスト");
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.X))
			{
				InformationManager.AddMessage("二行表示テスト\n二行表示テスト");
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.C))
			{
				TalkManager.Instance.StartTalk(this.testTalk, null);
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.L))
			{
				CellManager.Instance.Serialize();
			}
			if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
			{
				HK.Framework.SaveData.Save();
			}
		}
#endif

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

		public void DungeonClear(int type)
		{
			MineS.SaveData.Progress.ClearDungeon((GameDefine.DungeonType)type);
		}

		public void AddVisitShop()
		{
			MineS.SaveData.Progress.AddVisitShopCount(true);
		}

		public void AddVisitBlackSmith()
		{
			MineS.SaveData.Progress.AddVisitBlackSmithCount(true);
		}

		public void RemoveSaveData()
		{
			SaveDataEditor.Remove();
		}
	}
}