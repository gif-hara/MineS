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
		private int waitTurn = 1;

		public AbilityLongRangeAttack(CharacterData holder)
			: base(GameDefine.AbilityType.LongRangeAttack, holder, "LongRangeAttack")
		{
		}

		public override void OnTurnProgress()
		{
			if(this.waitTurn > 0)
			{
				this.waitTurn--;
				return;
			}

			CombatController.CombatLongRangeAttack(this.Holder);
		}
	}
}