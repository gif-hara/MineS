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
			/// ヒットポイント最大値を超えて回復.
			/// </summary>
			RecoveryHitPointUnlimit,

			/// <summary>
			/// マジックポイント回復.
			/// </summary>
			RecoveryMagicPoint,

			/// <summary>
			/// アーマー回復.
			/// </summary>
			RecoveryArmor,

			/// <summary>
			/// バフの効果を付与する.
			/// </summary>
			AddBuff,

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
			/// 精霊：一定ターンマジックポイントを消費せずに魔法が使用出来る.
			/// </summary>
			Spirit,

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
			/// 頭痛：魔法使用時のMP消費量が2倍になる.
			/// </summary>
			Headache,

			/// <summary>
			/// 鈍ら：与えるダメージが半分になる.
			/// </summary>
			Dull,
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
		}

		/// <summary>
		/// ターン経過タイプ.
		/// </summary>
		public enum TurnProgressType:int
		{
			CellClick,

			EnemyAttack,
		}

		public enum TrapType:int
		{
			Poison,
			Gout,
			Blur,
			Dull,
			Headache,
			Mine,
		}

		public const int AdjacentMax = 9;

		public const int MoneyMax = 999999;

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

			Debug.AssertFormat(false, "計算を間違えている可能性があります.");
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
			type == AbnormalStatusType.Spirit ||
			type == AbnormalStatusType.Xray ||
			type == AbnormalStatusType.TrapMaster ||
			type == AbnormalStatusType.Happiness;
		}
	}
}