using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public static class StoneStatueFactory
	{
		public static StoneStatue Create(GameDefine.StoneStatueType type)
		{
			switch(type)
			{
			case GameDefine.StoneStatueType.Souvenir:
				return new StoneStatue(type, "StoneStatueType.Souvenir");
			case GameDefine.StoneStatueType.InbariablyHit:
				return new StoneStatue(type, "StoneStatueType.InbariablyHit");
			case GameDefine.StoneStatueType.Identification:
				return new StoneStatue(type, "StoneStatueType.Identification");
			case GameDefine.StoneStatueType.Regeneration:
				return new StoneStatue(type, "StoneStatueType.Regeneration");
			case GameDefine.StoneStatueType.Poison:
				return new StoneStatue(type, "StoneStatueType.Poison");
			case GameDefine.StoneStatueType.Light:
				return new StoneStatue(type, "StoneStatueType.Light");
			case GameDefine.StoneStatueType.Illusion:
				return new StoneStatue(type, "StoneStatueType.Illusion");
			case GameDefine.StoneStatueType.SpringWater:
				return new StoneStatueSpringWater();
			case GameDefine.StoneStatueType.Happiness:
				return new StoneStatue(type, "StoneStatueType.Happiness");
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return null;
			}
		}
	}
}