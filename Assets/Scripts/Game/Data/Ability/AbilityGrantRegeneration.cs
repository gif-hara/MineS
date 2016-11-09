using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityGrantRegeneration : AbilityGrantAbnormalStatusBase
	{
		public AbilityGrantRegeneration(CharacterData holder)
			: base(GameDefine.AbilityType.GrantRegeneration, holder, "GrantRegeneration")
		{
		}

		protected override GameDefine.AbnormalStatusType GrantType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Regeneration;
			}
		}
	}
}