using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public static class AbilityFactory
	{
		public static List<AbilityBase> Create(List<GameDefine.AbilityType> types, CharacterData holder)
		{
			var result = new List<AbilityBase>();
			types.ForEach(a => result.Add(Create(a, holder)));

			return result;
		}

		public static AbilityBase Create(GameDefine.AbilityType type, CharacterData holder)
		{
			switch(type)
			{
			case GameDefine.AbilityType.Penetoration:
				return new AbilityBase(type, holder);
			case GameDefine.AbilityType.Mercy:
				return new AbilityMercy(holder);
			case GameDefine.AbilityType.Regeneration:
				return new AbilityRegeneration(holder);
			case GameDefine.AbilityType.Splash:
				return new AbilityBase(type, holder);
			case GameDefine.AbilityType.Absorption:
				return new AbilityBase(type, holder);
			case GameDefine.AbilityType.LongRangeAttack:
				return new AbilityLongRangeAttack(holder);
			case GameDefine.AbilityType.Reinforcement:
				return new AbilityBase(type, holder);
			default:
				Debug.AssertFormat(false, "不正な値です. type = " + type);
				return null;
			}
		}
	}
}