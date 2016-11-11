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
	public class AbilityMercy : AbilityBase
	{
		public AbilityMercy(CharacterData holder)
			: base(GameDefine.AbilityType.Mercy, holder, "Mercy")
		{
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			if(this.Holder == null)
			{
				return;
			}

			var recoveryValue = Calculator.GetMercyAbilityValue(this.Holder);
			EnemyManager.Instance.VisibleEnemies
				.Where(e => e != this.Holder)
				.ToList().ForEach(e => e.RecoveryHitPoint(recoveryValue, false));
		}
	}
}