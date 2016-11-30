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
	public class AbilityClairvoyance : AbilityBase
	{
		public AbilityClairvoyance(CharacterData holder)
			: base(GameDefine.AbilityType.Clairvoyance, holder, "Clairvoyance")
		{
		}

		public override void SetHolder(CharacterData holder)
		{
			base.SetHolder(holder);
			if(holder != null)
			{
				CellManager.Instance.OnUseXrayNotIdentification();
			}
		}
	}
}