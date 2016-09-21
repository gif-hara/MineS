using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class Buff : AbnormalStatusBase<GameDefine.BuffType, GameDefine.DebuffType>
	{
		public Buff(int remainingTurn, GameDefine.BuffType type)
			: base(remainingTurn, type)
		{
		}

		public override GameDefine.DebuffType OppositeType
		{
			get
			{
				switch(this.Type)
				{
				case GameDefine.BuffType.Regeneration:
					return GameDefine.DebuffType.Poison;
				case GameDefine.BuffType.Sharpness:
					return GameDefine.DebuffType.Dull;
				case GameDefine.BuffType.Curing:
					return GameDefine.DebuffType.Gout;
				case GameDefine.BuffType.Spirit:
					return GameDefine.DebuffType.Headache;
				case GameDefine.BuffType.Xray:
					return GameDefine.DebuffType.None;
				case GameDefine.BuffType.TrapMaster:
					return GameDefine.DebuffType.None;
				default:
					Debug.AssertFormat(false, "不正な値です. {0}", this.Type);
					return GameDefine.DebuffType.None;
				}
			}
		}
	}
}