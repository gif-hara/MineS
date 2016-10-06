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
			this.cellController.SetImage(this.Image);

			var player = PlayerManager.Instance.Data;
			if(player.FindAbnormalStatus(GameDefine.AbnormalStatusType.TrapMaster) || player.FindAbility(GameDefine.AbilityType.AvoidTrap))
			{
				return;
			}

			data.BindDeployDescription(new DeployDescriptionOnDescriptionData(this.DescriptionKey));
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

		public abstract string DescriptionKey{ get; }
	}
}