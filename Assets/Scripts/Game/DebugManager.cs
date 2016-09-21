using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DebugManager : MonoBehaviour
	{
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
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusRegeneration(5));
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusSharpness(5));
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusCuring(5));
			}
			if(Input.GetKeyDown(KeyCode.F))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusSpirit(5));
			}
			if(Input.GetKeyDown(KeyCode.G))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusTrapMaster(5));
			}
			if(Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusXray(5));
			}
			if(Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusHappiness(5));
			}
			if(Input.GetKeyDown(KeyCode.Z))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusPoison(5));
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusCurse(5));
			}
			if(Input.GetKeyDown(KeyCode.C))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusBlur(5));
			}
			if(Input.GetKeyDown(KeyCode.V))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusGout(5));
			}
			if(Input.GetKeyDown(KeyCode.B))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusHeadache(5));
			}
			if(Input.GetKeyDown(KeyCode.N))
			{
				PlayerManager.Instance.AddAbnormalStatus(new AbnormalStatusDull(5));
			}
		}
	}
}