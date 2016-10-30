using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateAnvilAction : CellClickActionBase
	{
		public CreateAnvilAction()
		{
		}

		public override void Invoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			data.BindCellClickAction(new InvokeAnvilAction());
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
				return TextureManager.Instance.anvilImage.Element;
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