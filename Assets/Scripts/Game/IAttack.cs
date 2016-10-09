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
		string Name{ get; }

		int Strength{ get; }

		int HitProbability{ get; }

		int Evasion{ get; }

		int Armor{ get; }

		int Experience{ get; }

		int Money{ get; }

		string ColorCode{ get; }

		bool FindAbility(GameDefine.AbilityType type);

		bool FindAbnormalStatus(GameDefine.AbnormalStatusType type);

		int GetAbilityNumber(GameDefine.AbilityType type);
	}
}