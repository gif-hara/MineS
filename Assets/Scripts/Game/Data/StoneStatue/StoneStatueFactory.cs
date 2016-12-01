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
		public static StoneStatue Create(GameDefine.StoneStatueType type, CellData cellData)
		{
			switch(type)
			{
			case GameDefine.StoneStatueType.Souvenir:
				return new StoneStatue(type, cellData);
			case GameDefine.StoneStatueType.InbariablyHit:
				return new StoneStatue(type, cellData);
			case GameDefine.StoneStatueType.Identification:
				return new StoneStatue(type, cellData);
			case GameDefine.StoneStatueType.Regeneration:
				return new StoneStatue(type, cellData);
			case GameDefine.StoneStatueType.Poison:
				return new StoneStatue(type, cellData);
			case GameDefine.StoneStatueType.Light:
				return new StoneStatueLight(cellData);
			case GameDefine.StoneStatueType.SpringWater:
				return new StoneStatueSpringWater(cellData);
			case GameDefine.StoneStatueType.Happiness:
				return new StoneStatue(type, cellData);
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return null;
			}
		}
	}
}