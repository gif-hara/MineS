using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class InvokeTrapActionBase : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			if(PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.TrapMaster))
			{
				return;
			}

			this.InternalInvoke(data);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Trap;
			}
		}

		public abstract void InternalInvoke(CellData data);
	}
}