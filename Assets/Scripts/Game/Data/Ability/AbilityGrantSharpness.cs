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
	public class AbilityGrantSharpness : AbilityGrantAbnormalStatusBase
	{
		public AbilityGrantSharpness(CharacterData holder)
			: base(GameDefine.AbilityType.GrantSharpness, holder, "GrantSharpness")
		{
		}

		protected override GameDefine.AbnormalStatusType GrantType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Sharpness;
			}
		}
	}
}