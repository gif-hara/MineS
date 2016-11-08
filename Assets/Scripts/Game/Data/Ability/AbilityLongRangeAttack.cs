using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityLongRangeAttack : AbilityBase
	{
		public AbilityLongRangeAttack(CharacterData holder)
			: base(GameDefine.AbilityType.LongRangeAttack, holder, "LongRangeAttack")
		{
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			if(this.Holder == null)
			{
				return;
			}
			CombatController.CombatLongRangeAttack(PlayerManager.Instance.Data, this.Holder);
		}
	}
}