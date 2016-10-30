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

		public override void Serialize(int y, int x)
		{
		}

		public override void Deserialize(int y, int x)
		{
		}
	}
}