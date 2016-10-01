using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public interface IAttack
	{
		int Strength{ get; }

		int HitProbability{ get; }

		int Evasion{ get; }

		bool FindAbility(GameDefine.AbilityType type);

		bool FindAbnormalStatus(GameDefine.AbnormalStatusType type);

		int GetAbilityNumber(GameDefine.AbilityType type);
	}
}