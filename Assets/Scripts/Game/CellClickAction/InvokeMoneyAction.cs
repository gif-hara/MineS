using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeMoneyAction : CellClickActionBase
	{
		private int money;

		public InvokeMoneyAction(int money)
		{
			this.money = money;
		}

		public override void Invoke(CellData data)
		{
			data.Controller.SetImage(null);
			data.BindCellClickAction(null);
			data.BindDeployDescription(null);
			PlayerManager.Instance.AddMoney(this.money, true);
			InformationManager.AcquireMoney(this.money);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Anvil;
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