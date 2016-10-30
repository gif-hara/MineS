using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeAbnormalStatusTrapAction : InvokeTrapActionBase
	{
		private GameDefine.AbnormalStatusType type;

		public InvokeAbnormalStatusTrapAction()
		{
		}

		public InvokeAbnormalStatusTrapAction(GameDefine.AbnormalStatusType type)
		{
			this.type = type;
		}

		public InvokeAbnormalStatusTrapAction(GameDefine.TrapType type)
		{
			this.type = GameDefine.ConvertTrapTypeToAbnormalStatusType(type);
		}

		public override void InternalInvoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			PlayerManager.Instance.AddAbnormalStatus(this.type, GameDefine.AbnormalStatusTrapRemainingTurn, 1);
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.trap.Get(this.type);
			}
		}

		public override string DescriptionKey
		{
			get
			{
				switch(this.type)
				{
				case GameDefine.AbnormalStatusType.Poison:
					return "TrapPoison";
				case GameDefine.AbnormalStatusType.Blur:
					return "TrapBlur";
				case GameDefine.AbnormalStatusType.Dull:
					return "TrapDull";
				case GameDefine.AbnormalStatusType.Gout:
					return "TrapGout";
				default:
					Debug.AssertFormat(false, "不正な値です. type = {0}", type);
					return "";
				}
			}
		}

		public override void Serialize(int y, int x)
		{
			base.Serialize(y, x);
			HK.Framework.SaveData.SetInt(this.GetTypeSerializeKeyName(y, x), (int)this.type);
		}

		public override void Deserialize(int y, int x)
		{
			base.Deserialize(y, x);
			this.type = (GameDefine.AbnormalStatusType)HK.Framework.SaveData.GetInt(this.GetTypeSerializeKeyName(y, x));
		}

		private string GetTypeSerializeKeyName(int y, int x)
		{
			return string.Format("InvokeAbnormalStatusTrapActionType_{0}_{1}", y, x);
		}
	}
}