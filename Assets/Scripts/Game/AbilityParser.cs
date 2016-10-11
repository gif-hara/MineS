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
	public class AbilityParser
	{
		public static List<GameDefine.AbilityType> Parse(string data)
		{
			if(string.IsNullOrEmpty(data))
			{
				return new List<GameDefine.AbilityType>();
			}

			return data.Split(' ').Select(s => Get(s)).ToList();
		}

		private static GameDefine.AbilityType Get(string name)
		{
			switch(name)
			{
			case "貫通":
				return GameDefine.AbilityType.Penetoration;
			case "慈悲":
				return GameDefine.AbilityType.Mercy;
			case "自己再生":
				return GameDefine.AbilityType.Regeneration;
			case "跳返":
				return GameDefine.AbilityType.Splash;
			case "吸収":
				return GameDefine.AbilityType.Absorption;
			case "遠距離攻撃":
				return GameDefine.AbilityType.LongRangeAttack;
			case "三子教訓":
				return GameDefine.AbilityType.Reinforcement;
			case "連撃":
				return GameDefine.AbilityType.ContinuousAttack;
			case "習熟":
				return GameDefine.AbilityType.Proficiency;
			case "巻上":
				return GameDefine.AbilityType.Hoist;
			case "改修":
				return GameDefine.AbilityType.Repair;
			case "幸運":
				return GameDefine.AbilityType.Fortune;
			case "匠":
				return GameDefine.AbilityType.Artisan;
			case "精巧":
				return GameDefine.AbilityType.Exquisite;
			case "回避":
				return GameDefine.AbilityType.Evasion;
			case "命中":
				return GameDefine.AbilityType.HitProbability;
			case "五右衛門":
				return GameDefine.AbilityType.Goemon;
			case "窃盗":
				return GameDefine.AbilityType.Theft;
			case "必中":
				return GameDefine.AbilityType.InbariablyHit;
			case "捨身":
				return GameDefine.AbilityType.RiskOfLife;
			case "増進":
				return GameDefine.AbilityType.Enhancement;
			case "減退":
				return GameDefine.AbilityType.Weak;
			case "免疫":
				return GameDefine.AbilityType.Immunity;
			case "伝染":
				return GameDefine.AbilityType.Infection;
			case "罠避":
				return GameDefine.AbilityType.AvoidTrap;
			case "薬師":
				return GameDefine.AbilityType.HealingBuddha;
			case "毒塗":
				return GameDefine.AbilityType.PoisonPainted;
			case "放心":
				return GameDefine.AbilityType.Absentmindedness;
			case "秘孔突き":
				return GameDefine.AbilityType.VitalsPoke;
			case "刃毀":
				return GameDefine.AbilityType.BladeBroken;
			case "錯乱":
				return GameDefine.AbilityType.Derangement;
			case "脅迫":
				return GameDefine.AbilityType.Intimidation;
			case "呪縛":
				return GameDefine.AbilityType.Curse;
			case "雄叫び":
				return GameDefine.AbilityType.WarCry;
			case "回復":
				return GameDefine.AbilityType.Recovery;
			case "防護":
				return GameDefine.AbilityType.Protection;
			case "先手":
				return GameDefine.AbilityType.Initiative;
			case "千里眼":
				return GameDefine.AbilityType.Clairvoyance;
			case "分裂":
				return GameDefine.AbilityType.Division;
			case "召喚":
				return GameDefine.AbilityType.Summon;
			case "転生":
				return GameDefine.AbilityType.Reincarnation;
			case "挑発":
				return GameDefine.AbilityType.Provocation;
			case "血清":
				return GameDefine.AbilityType.Serum;
			case "集中":
				return GameDefine.AbilityType.Concentration;
			case "快癒":
				return GameDefine.AbilityType.CompleteRecovery;
			case "鍍金":
				return GameDefine.AbilityType.Plating;
			case "勇気":
				return GameDefine.AbilityType.Bravery;
			case "魔除":
				return GameDefine.AbilityType.Talisman;
			case "気付":
				return GameDefine.AbilityType.Care;
			default:
				Debug.AssertFormat(false, "不正な値です name = {0}", name);
				return GameDefine.AbilityType.Penetoration;
			}
		}
	}
}