using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class VisitBlackSmithAction : CellClickActionBase
	{
		private bool isTown;

		public VisitBlackSmithAction()
		{
			this.isTown = false;
		}

		public VisitBlackSmithAction(bool isTown)
		{
			this.isTown = isTown;
		}

		public override void Invoke(CellData data)
		{
			if(this.isTown && MineS.SaveData.Progress.VisitTownBlackSmithCount <= 0)
			{
				BlackSmithManager.Instance.OpenNPCUI();
				BlackSmithManager.Instance.InvokeFirstTalkTown(() =>
				{
					BlackSmithManager.Instance.OpenUI();
				});
			}
			else if(!this.isTown && MineS.SaveData.Progress.VisitBlackSmithCount <= 0)
			{
				BlackSmithManager.Instance.OpenNPCUI();
				BlackSmithManager.Instance.InvokeFirstTalk(() =>
				{
					BlackSmithManager.Instance.OpenUI();
				});
			}
			else
			{
				BlackSmithManager.Instance.OpenUI();
			}
			MineS.SaveData.Progress.AddVisitBlackSmithCount(this.isTown);
		}

		public override void SetCellController(CellController cellController)
		{
			cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("BlackSmith"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.BlackSmith;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.blackSmith.Element;
			}
		}

		public override void Serialize(int y, int x)
		{
			HK.Framework.SaveData.SetInt(this.GetIsTownSerializeKeyName(y, x), this.isTown ? 1 : 0);
		}

		public override void Deserialize(int y, int x)
		{
			this.isTown = HK.Framework.SaveData.GetInt(this.GetIsTownSerializeKeyName(y, x)) == 1;
		}

		private string GetIsTownSerializeKeyName(int y, int x)
		{
			return string.Format("VisitBlackSmithActionIsTown_{0}_{1}", y, x);
		}
	}
}