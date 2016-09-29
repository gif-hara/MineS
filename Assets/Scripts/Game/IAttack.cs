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

		bool FindAbility(GameDefine.AbilityType type);

		bool FindAbnormalStatus(GameDefine.AbnormalStatusType type);
	}
}