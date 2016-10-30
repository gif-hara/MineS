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

		private bool isTown;

		public VisitShopAction()
		{
			
		}

		public VisitShopAction(bool isTown)
		{
			var dungeonManager = DungeonManager.Instance;
			this.goods = dungeonManager.CurrentData.CreateShopInventory(dungeonManager.Floor);
			this.isTown = isTown;
		}

		public override void Invoke(CellData data)
		{
			if(this.isTown && MineS.SaveData.Progress.VisitTownShopCount <= 0)
			{
				ShopManager.Instance.OpenNPCUI();
				ShopManager.Instance.InvokeFirstTalkTown(() =>
				{
					ShopManager.Instance.OpenUI(this.goods);
				});
			}
			else if(!this.isTown && MineS.SaveData.Progress.VisitShopCount <= 0)
			{
				ShopManager.Instance.OpenNPCUI();
				ShopManager.Instance.InvokeFirstTalk(() =>
				{
					ShopManager.Instance.OpenUI(this.goods);
				});
			}
			else
			{
				ShopManager.Instance.OpenUI(this.goods);
			}
			MineS.SaveData.Progress.AddVisitShopCount(this.isTown);
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

		public override void Serialize(int y, int x)
		{
			HK.Framework.SaveData.SetInt(this.GetIsTownSerializeKeyName(y, x), this.isTown ? 1 : 0);
			this.goods.Serialize(this.GetGoodsSerializeKeyName(y, x));
		}

		public override void Deserialize(int y, int x)
		{
			this.isTown = HK.Framework.SaveData.GetInt(this.GetIsTownSerializeKeyName(y, x)) == 1;
			this.goods = new Inventory(null, GameDefine.ShopInventoryMax);
			this.goods.Deserialize(this.GetGoodsSerializeKeyName(y, x));
		}

		private string GetIsTownSerializeKeyName(int y, int x)
		{
			return string.Format("VisitShopActionIsTown_{0}_{1}", y, x);
		}

		private string GetGoodsSerializeKeyName(int y, int x)
		{
			return string.Format("VisitShopActionGoods_{0}_{1}", y, x);
		}
	}
}