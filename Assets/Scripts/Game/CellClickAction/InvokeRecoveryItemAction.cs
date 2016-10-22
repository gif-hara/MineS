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
			data.BindDeployDescription(null);
			var value = GameDefine.RecoveryItemRecovery;
			PlayerManager.Instance.RecoveryHitPoint(value, false);
			InformationManager.OnRecovery(value);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.RecoveryItem;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.recoveryItem.Element;
			}
		}
	}
}