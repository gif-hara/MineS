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
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Regeneration, 5);
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Sharpness, 5);
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Curing, 5);
			}
			if(Input.GetKeyDown(KeyCode.F))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Spirit, 5);
			}
			if(Input.GetKeyDown(KeyCode.G))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.TrapMaster, 5);
			}
			if(Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Xray, 5);
			}
			if(Input.GetKeyDown(KeyCode.J))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Happiness, 5);
			}
			if(Input.GetKeyDown(KeyCode.Z))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Poison, 5);
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Blur, 5);
			}
			if(Input.GetKeyDown(KeyCode.C))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Gout, 5);
			}
			if(Input.GetKeyDown(KeyCode.V))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Headache, 5);
			}
			if(Input.GetKeyDown(KeyCode.B))
			{
				PlayerManager.Instance.AddAbnormalStatus(GameDefine.AbnormalStatusType.Dull, 5);
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