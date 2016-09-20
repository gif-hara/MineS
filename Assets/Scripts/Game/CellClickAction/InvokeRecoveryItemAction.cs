using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeRecoveryItemAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetImage(null);
			data.BindCellClickAction(null);
			PlayerManager.Instance.Recovery(GameDefine.RecoveryItemRecovery);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.RecoveryItem;
			}
		}
	}
}