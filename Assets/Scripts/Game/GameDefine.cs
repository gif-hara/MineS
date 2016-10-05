using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class GameDefine
	{
		public enum EventType:int
		{
			/// <summary>
			/// 無し.
			/// </summary>
			None,

			/// <summary>
			/// 階段.
			/// </summary>
			Stair,

			/// <summary>
			/// 回復アイテム.
			/// </summary>
			RecoveryItem,

			/// <summary>
			/// 敵.
			/// </summary>
			Enemy,

			/// <summary>
			/// アイテム.
			/// </summary>
			Item,

			/// <summary>
			/// 罠.
			/// </summary>
			Trap,
		}

		/// <summary>
		/// アクション実行タイプ.
		/// </summary>
		public enum ActionableType:int
		{
			/// <summary>
			/// アクション可能.
			/// </summary>
			OK,
			/// <summary>
			/// まだ踏めない.
			/// </summary>
			NotStep,
			/// <summary>
			/// ロックされている.
			/// </summary>
			Lock,
		}

		public enum AcquireItemResultType:int
		{
			/// <summary>
			/// 取得した.
			/// </summary>
			Acquired,

			/// <summary>
			/// インベントリに空きが無かった.
			/// </summary>
			Full,
		}

		public enum AdjacentType:int
		{
			Left,
			LeftTop,
			Top,
			RightTop,
			Right,
			RightBottom,
			Bottom,
			LeftBottom,
		}

		public enum RareType:int
		{
			Normal,
			Rare,
			SuperRare,
			Legend,
		}

		public enum ItemType:int
		{
			UsableItem,
			Weapon,
			Shield,
			Helmet,
			Body,
			Glove,
			Leg,
			Accessory,
		}

		public enum UsableItemType:int
		{
			/// <summary>
			/// ヒットポイント回復.
			/// </summary>
			RecoveryHitPointLimit,

			/// <summary>
			/// アーマー回復.
			/// </summary>
			RecoveryArmor,

			/// <summary>
			/// バフの効果を付与する.
			/// </summary>
			AddBuff,

			/// <summary>
			/// デバフの効果を付与する.
			/// </summary>
			AddDebuff,

			/// <summary>
			/// デバフの効果を削除する.
			/// </summary>
			RemoveDebuff,

			/// <summary>
			/// ファイアボール：敵にダメージを与える.
			/// </summary>
			Fire,

			/// <summary>
			/// ネイルダウン：全てのセルを表示する.
			/// </summary>
			NailDown,

			/// <summary>
			/// コーリングオフ：罠を全て解除する.
			/// </summary>
			CallingOff,

			/// <summary>
			/// カースボール：敵の攻撃力を下げる.
			/// </summary>
			Carse,

			/// <summary>
			/// ドレインボール：敵のヒットポイントを吸収する.
			/// </summary>
			Drain,

			/// <summary>
			/// ブレイクボール：敵のアーマーを0にする.
			/// </summary>
			Brake,

			/// <summary>
			/// デスボール：雑魚敵を倒す.
			/// </summary>
			Death,
		}

		public enum AbnormalStatusType:int
		{
			/// <summary>
			/// 無し.
			/// </summary>
			None,

			/// <summary>
			/// 再生：ヒットポイントが徐々に回復する.
			/// </summary>
			Regeneration,

			/// <summary>
			/// 鋭利：与えるダメージを2倍にする.
			/// </summary>
			Sharpness,

			/// <summary>
			/// 硬化：受けるダメージを半分にする.
			/// </summary>
			Curing,

			/// <summary>
			/// 透視：未識別のセルを透視する.
			/// </summary>
			Xray,

			/// <summary>
			/// 罠師：罠の影響を受けない.
			/// </summary>
			TrapMaster,

			/// <summary>
			/// 幸福：取得経験値が2倍になる.
			/// </summary>
			Happiness,

			/// <summary>
			/// 毒：ターン終了時にダメージを受ける.
			/// </summary>
			Poison,

			/// <summary>
			/// 呆け：取得経験値が半分になる.
			/// </summary>
			Blur,

			/// <summary>
			/// 痛風：受けるダメージが2倍になる.
			/// </summary>
			Gout,

			/// <summary>
			/// 鈍ら：与えるダメージが半分になる.
			/// </summary>
			Dull,

			/// <summary>
			/// 恐怖：攻撃できなくなる.
			/// </summary>
			Fear,

			/// <summary>
			/// 封印：特殊能力が発動しなくなる.
			/// </summary>
			Seal,

			/// <summary>
			/// 混乱：ターンを消費する行動がランダムに行われてしまう.
			/// </summary>
			Confusion,
		}

		public enum AbilityType:int
		{
			/// <summary>
			/// 貫通：アーマーを無視して攻撃する.
			/// </summary>
			Penetoration,

			/// <summary>
			/// 慈悲：ターン終了時に他の敵のHPを回復する.
			/// </summary>
			Mercy,

			/// <summary>
			/// 自己再生：ターン終了時にHPを回復する.
			/// </summary>
			Regeneration,

			/// <summary>
			/// 跳返：受けたダメージの半分を相手に返す.
			/// </summary>
			Splash,

			/// <summary>
			/// 吸収：与えたダメージの半分を吸収する.
			/// </summary>
			Absorption,

			/// <summary>
			/// 遠距離攻撃：ターン終了時に攻撃する.
			/// </summary>
			LongRangeAttack,

			/// <summary>
			/// 三子教訓：他の敵の数だけ攻撃力が上昇する.
			/// </summary>
			Reinforcement,

			/// <summary>
			/// 連撃：攻撃した際に他に敵がいる場合は半分のダメージを与える.
			/// </summary>
			ContinuousAttack,

			/// <summary>
			/// 習熟：取得する経験値が上昇する.
			/// </summary>
			Proficiency,

			/// <summary>
			/// 巻上：取得するお金が上昇する.
			/// </summary>
			Hoist,

			/// <summary>
			/// 改修：敵を倒した際にアーマーが回復する.
			/// </summary>
			Repair,

			/// <summary>
			/// 幸運：敵を倒した際のアイテムドロップ率が上昇する.
			/// </summary>
			Fortune,

			/// <summary>
			/// 匠：攻撃力が上昇する.
			/// </summary>
			Artisan,

			/// <summary>
			/// 精巧：アーマーが上昇する.
			/// </summary>
			Exquisite,

			/// <summary>
			/// 回避：回避率が上昇する.
			/// </summary>
			Evasion,

			/// <summary>
			/// 命中：命中率が上昇する.
			/// </summary>
			HitProbability,

			/// <summary>
			/// 五右衛門：与えたダメージから一定数のお金を取得する.
			/// </summary>
			Goemon,

			/// <summary>
			/// 窃盗：攻撃した際に道具袋に空きがある場合に一定確率でアイテムを取得する.
			/// </summary>
			Theft,

			/// <summary>
			/// 必中：攻撃が必ず当たる.
			/// </summary>
			InbariablyHit,

			/// <summary>
			/// 捨身：アーマーを消費して攻撃力を上昇する.
			/// </summary>
			RiskOfLife,

			/// <summary>
			/// 増進：バフの継続ターンを増やす.
			/// </summary>
			Enhancement,

			/// <summary>
			/// 減退：バフの継続ターンが減少する.
			/// </summary>
			Weak,

			/// <summary>
			/// 免疫：デバフの継続ターンを減らす.
			/// </summary>
			Immunity,

			/// <summary>
			/// 伝染：デバフの継続ターンを増やす.
			/// </summary>
			Infection,

			/// <summary>
			/// 罠避：罠に引っかからなくなる.
			/// </summary>
			AvoidTrap,

			/// <summary>
			/// 薬師：回復系アイテムの効果が上昇する.
			/// </summary>
			HealingBuddha,

			/// <summary>
			/// 毒塗：ダメージを与えた際に一定確率でデバフの毒を付与する.
			/// </summary>
			PoisonPainted,

			/// <summary>
			/// 放心：ダメージを与えた際に一定確率でデバフの呆けを付与する.
			/// </summary>
			Absentmindedness,

			/// <summary>
			/// 秘孔突き：ダメージを与えた際に一定確率でデバフの痛風を付与する.
			/// </summary>
			VitalsPoke,

			/// <summary>
			/// 刃毀：ダメージを与えた際に一定確率でデバフの鈍らを付与する.
			/// </summary>
			BladeBroken,

			/// <summary>
			/// 錯乱：ダメージを与えた際に一定確率でデバフの混乱を付与する.
			/// </summary>
			Derangement,

			/// <summary>
			/// 脅迫：ダメージを与えた際に一定確率でデバフの恐怖を付与する.
			/// </summary>
			Intimidation,

			/// <summary>
			/// 呪縛：ダメージを与えた際に一定確率でデバフの封印を付与する.
			/// </summary>
			Curse,

			/// <summary>
			/// 雄叫び：識別状態になった際に他の敵も識別されてしまう（敵のみ）.
			/// </summary>
			WarCry,

			/// <summary>
			/// 回復：ダメージを与えた際にヒットポイントが回復する.
			/// </summary>
			Recovery,

			/// <summary>
			/// 防護：受けるダメージを軽減する.
			/// </summary>
			Protection,
		}

		/// <summary>
		/// ターン経過タイプ.
		/// </summary>
		public enum TurnProgressType:int
		{
			CellClick,

			EnemyAttack,
		}

		/// <summary>
		/// 罠タイプ.
		/// </summary>
		public enum TrapType:int
		{
			Poison,
			Gout,
			Blur,
			Dull,
			Headache,
			Mine,
		}

		public enum AttackResultType:int
		{
			Hit,
			Miss,
			MissByFear
		}

		public enum SelectItemDecideType:int
		{
			/// <summary>
			/// 使う.
			/// </summary>
			Use,

			/// <summary>
			/// 装備する.
			/// </summary>
			Equipment,

			/// <summary>
			/// 外す.
			/// </summary>
			Remove,

			/// <summary>
			/// 投げる.
			/// </summary>
			Throw,

			/// <summary>
			/// 置く.
			/// </summary>
			Put,

			/// <summary>
			/// 説明.
			/// </summary>
			Description,

			/// <summary>
			/// キャンセル.
			/// </summary>
			Cancel,
		}

		public const int AdjacentMax = 9;

		public const int MoneyMax = 9999999;

		public const int InventoryItemMax = 8;

		public const int RecoveryItemRecovery = 5;

		public const int ArmorMax = 999;

		public const int AbnormalStatusTrapRemainingTurn = 5;

		public static int Lottery<P>(List<P> elements) where P : IProbability
		{
			int probabilityMax = 0;
			for(int i = 0; i < elements.Count; i++)
			{
				probabilityMax += elements[i].Probability;
			}

			int probability = Random.Range(0, probabilityMax);
			int currentProbability = 0;
			for(int i = 0; i < elements.Count; i++)
			{
				var table = elements[i];
				if(probability >= currentProbability && probability < (currentProbability + table.Probability))
				{
					return i;
				}
				currentProbability += table.Probability;
			}

			Debug.AssertFormat(false, "抽選処理に失敗しました. 計算を間違えている可能性があります.");
			return -1;
		}

		public static bool IsEquipment(ItemType itemType)
		{
			return itemType == ItemType.Accessory
			|| itemType == ItemType.Body
			|| itemType == ItemType.Glove
			|| itemType == ItemType.Helmet
			|| itemType == ItemType.Leg
			|| itemType == ItemType.Shield
			|| itemType == ItemType.Weapon;
		}

		public static bool IsBuff(GameDefine.AbnormalStatusType type)
		{
			Debug.AssertFormat(type != AbnormalStatusType.None, "不正な値です.");

			return type == AbnormalStatusType.Regeneration ||
			type == AbnormalStatusType.Sharpness ||
			type == AbnormalStatusType.Curing ||
			type == AbnormalStatusType.Xray ||
			type == AbnormalStatusType.TrapMaster ||
			type == AbnormalStatusType.Happiness;
		}

		public static GameDefine.AbnormalStatusType ConvertTrapTypeToAbnormalStatusType(GameDefine.TrapType type)
		{
			switch(type)
			{
			case GameDefine.TrapType.Poison:
				return GameDefine.AbnormalStatusType.Poison;
			case GameDefine.TrapType.Blur:
				return GameDefine.AbnormalStatusType.Blur;
			case GameDefine.TrapType.Dull:
				return GameDefine.AbnormalStatusType.Dull;
			case GameDefine.TrapType.Gout:
				return GameDefine.AbnormalStatusType.Gout;
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return GameDefine.AbnormalStatusType.None;
			}
		}

		public static bool IsAbnormalTrap(GameDefine.TrapType type)
		{
			return type == TrapType.Blur ||
			type == TrapType.Dull ||
			type == TrapType.Gout ||
			type == TrapType.Headache ||
			type == TrapType.Poison;
		}
	}
}