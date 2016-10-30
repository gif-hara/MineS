using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeAnvilAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetImage(null);
			data.BindCellClickAction(null);
			data.BindDeployDescription(null);
			var value = GameDefine.RecoveryItemRecovery;
			PlayerManager.Instance.RecoveryArmor(value, true);
			InformationManager.OnRecoveryArmor(value);
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			this.cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
			base.SetCellData(data);
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("Anvil"));
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