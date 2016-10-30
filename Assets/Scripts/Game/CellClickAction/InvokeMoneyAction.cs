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

		public InvokeMoneyAction()
		{
			this.money = 0;
		}

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

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			this.cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
			base.SetCellData(data);
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("Money"));
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
				return TextureManager.Instance.moneyImage.Element;
			}
		}

		public override void Serialize(int y, int x)
		{
			HK.Framework.SaveData.SetInt(this.GetSerializeKeyName(y, x), this.money);
		}

		public override void Deserialize(int y, int x)
		{
			this.money = HK.Framework.SaveData.GetInt(this.GetSerializeKeyName(y, x));
		}

		private string GetSerializeKeyName(int y, int x)
		{
			return string.Format("InvokeMoneyActionMoney_{0}_{1}", y, x);
		}
	}
}