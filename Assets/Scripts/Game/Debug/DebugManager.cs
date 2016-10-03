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
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Regeneration, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Sharpness, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Curing, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.F))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.TrapMaster, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.G))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Xray, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Happiness, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.Z))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Poison, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Blur, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.C))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Gout, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.V))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Dull, abnormalStatusRemainingTurn, 0);
			}
			if(Input.GetKeyDown(KeyCode.B))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Confusion, abnormalStatusRemainingTurn, 0);
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
	}
}