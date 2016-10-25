using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class VisitWareHouseAction : CellClickActionBase
	{
		public VisitWareHouseAction()
		{
		}

		public override void Invoke(CellData data)
		{
			WareHouseManager.Instance.OpenUI();
		}

		public override void SetCellController(CellController cellController)
		{
			cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("WareHouse"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.WareHouse;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.wareHouse.Element;
			}
		}
	}
}