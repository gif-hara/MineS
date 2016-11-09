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
	public class AbilityGrantCuring : AbilityGrantAbnormalStatusBase
	{
		public AbilityGrantCuring(CharacterData holder)
			: base(GameDefine.AbilityType.GrantCuring, holder, "GrantCuring")
		{
		}

		protected override GameDefine.AbnormalStatusType GrantType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Curing;
			}
		}
	}
}