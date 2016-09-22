using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityMercy : AbilityBase
	{
		public AbilityMercy(CharacterData holder)
			: base(GameDefine.AbilityType.Mercy, holder)
		{
		}

		public override void OnTurnProgress()
		{
			var recoveryValue = Calculator.GetRegenerationValue(this.Holder.HitPointMax);
			foreach(var e in EnemyManager.Instance.Enemies)
			{
				if(e.Value == this.Holder || e.Value == null || !e.Key.IsIdentification)
				{
					continue;
				}

				e.Value.RecoveryHitPoint(recoveryValue, false);
			}
		}
	}
}