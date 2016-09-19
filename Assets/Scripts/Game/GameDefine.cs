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

		public const int AdjacentMax = 9;

		public const int MoneyMax = 999999;
	}
}