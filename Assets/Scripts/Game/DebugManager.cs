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
				PlayerManager.Instance.DebugRecoveryArmor();
			}
			if(Input.GetKeyDown(KeyCode.A))
			{
				PlayerManager.Instance.AddBuff(new Buff(5, GameDefine.BuffType.Regeneration));
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				PlayerManager.Instance.AddBuff(new Buff(5, GameDefine.BuffType.Curing));
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				PlayerManager.Instance.AddBuff(new Buff(5, GameDefine.BuffType.Sharpness));
			}
			if(Input.GetKeyDown(KeyCode.F))
			{
				PlayerManager.Instance.AddBuff(new Buff(5, GameDefine.BuffType.Spirit));
			}
			if(Input.GetKeyDown(KeyCode.G))
			{
				PlayerManager.Instance.AddBuff(new Buff(5, GameDefine.BuffType.TrapMaster));
			}
			if(Input.GetKeyDown(KeyCode.H))
			{
				PlayerManager.Instance.AddBuff(new Buff(5, GameDefine.BuffType.Xray));
			}
			if(Input.GetKeyDown(KeyCode.Z))
			{
				PlayerManager.Instance.AddDebuff(new Debuff(5, GameDefine.DebuffType.Poison));
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				PlayerManager.Instance.AddDebuff(new Debuff(5, GameDefine.DebuffType.Curse));
			}
			if(Input.GetKeyDown(KeyCode.C))
			{
				PlayerManager.Instance.AddDebuff(new Debuff(5, GameDefine.DebuffType.Blur));
			}
			if(Input.GetKeyDown(KeyCode.V))
			{
				PlayerManager.Instance.AddDebuff(new Debuff(5, GameDefine.DebuffType.Gout));
			}
			if(Input.GetKeyDown(KeyCode.B))
			{
				PlayerManager.Instance.AddDebuff(new Debuff(5, GameDefine.DebuffType.Headache));
			}
			if(Input.GetKeyDown(KeyCode.N))
			{
				PlayerManager.Instance.AddDebuff(new Debuff(5, GameDefine.DebuffType.Dull));
			}
		}
	}
}