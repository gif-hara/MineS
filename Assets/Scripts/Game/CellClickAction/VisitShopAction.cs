using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class VisitShopAction : CellClickActionBase
	{
		private Inventory goods;

		public VisitShopAction()
		{
			var dungeonManager = DungeonManager.Instance;
			this.goods = dungeonManager.CurrentData.CreateShopInventory(dungeonManager.Floor);
		}

		public override void Invoke(CellData data)
		{
			ShopManager.Instance.OpenUI(this.goods);
		}

		public override void SetCellController(CellController cellController)
		{
			cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("Shop"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Shop;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.shop.Element;
			}
		}
	}
}