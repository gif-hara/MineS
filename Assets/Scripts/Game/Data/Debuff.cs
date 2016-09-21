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
	public class Debuff : AbnormalStatusBase<GameDefine.DebuffType, GameDefine.BuffType>
	{
		public Debuff(int remainingTurn, GameDefine.DebuffType type)
			: base(remainingTurn, type)
		{
		}

		public override GameDefine.BuffType OppositeType
		{
			get
			{
				switch(this.Type)
				{
				case GameDefine.DebuffType.Poison:
					return GameDefine.BuffType.Regeneration;
				case GameDefine.DebuffType.Curse:
					return GameDefine.BuffType.None;
				case GameDefine.DebuffType.Blur:
					return GameDefine.BuffType.None;
				case GameDefine.DebuffType.Gout:
					return GameDefine.BuffType.Curing;
				case GameDefine.DebuffType.Headache:
					return GameDefine.BuffType.Spirit;
				case GameDefine.DebuffType.Dull:
					return GameDefine.BuffType.Sharpness;
				default:
					Debug.AssertFormat(false, "不正な値です. {0}", this.Type);
					return GameDefine.BuffType.None;
				}
			}
		}

	}
}