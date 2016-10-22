using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateMoneyAction : CellClickActionBase
	{
		public CreateMoneyAction()
		{
		}

		public override void Invoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			data.BindCellClickAction(new InvokeMoneyAction(DungeonManager.Instance.CurrentDataAsDungeon.AcquireMoneyRange.Random));
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("Money"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Money;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.moneyImage.Element;
			}
		}
	}
}