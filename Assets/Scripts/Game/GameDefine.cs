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

		public const int AdjacentMax = 9;

		public const int MoneyMax = 999999;

		public const int InventoryItemMax = 8;

		public const int RecoveryItemRecovery = 5;

		public const int ArmorMax = 999;

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
			return
				itemType == ItemType.Accessory
			|| itemType == ItemType.Body
			|| itemType == ItemType.Glove
			|| itemType == ItemType.Helmet
			|| itemType == ItemType.Leg
			|| itemType == ItemType.Shield
			|| itemType == ItemType.Weapon;
		}
	}
}