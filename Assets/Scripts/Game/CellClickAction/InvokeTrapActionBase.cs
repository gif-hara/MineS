using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class InvokeTrapActionBase : CellClickActionBase
	{
		private bool isInvoke = true;

		public override void Invoke(CellData data)
		{
			this._InternalInvoke(data);
			this.InternalInvoke(data);
		}

		public override void InvokeFromLightStoneStatue(CellData cellData)
		{
			this._InternalInvoke(cellData);
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
		}

		public override void SetCellData(CellData data)
		{
			base.SetCellData(data);
			if(data.IsIdentification)
			{
				this.cellController.SetImage(this.Image);
				data.BindDeployDescription(new DeployDescriptionOnDescriptionData(this.DescriptionKey));
			}
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Trap;
			}
		}

		public abstract void InternalInvoke(CellData data);

		public abstract string DescriptionKey{ get; }

		public override void Serialize(int y, int x)
		{
			HK.Framework.SaveData.SetInt(this.GetIsInvokeSerializeKeyName(y, x), this.isInvoke ? 1 : 0);
		}

		public override void Deserialize(int y, int x)
		{
			this.isInvoke = HK.Framework.SaveData.GetInt(this.GetIsInvokeSerializeKeyName(y, x)) == 1;
		}

		private void _InternalInvoke(CellData data)
		{
			if(!this.isInvoke)
			{
				return;
			}

			this.isInvoke = false;
			this.cellController.SetImage(this.Image);

			var player = PlayerManager.Instance.Data;
			if(player.FindAbnormalStatus(GameDefine.AbnormalStatusType.TrapMaster) || player.FindAbility(GameDefine.AbilityType.AvoidTrap))
			{
				return;
			}

			data.BindDeployDescription(new DeployDescriptionOnDescriptionData(this.DescriptionKey));
		}

		private string GetIsInvokeSerializeKeyName(int y, int x)
		{
			return string.Format("InvokeTrapActionIsInvoke_{0}_{1}", y, x);
		}
	}
}