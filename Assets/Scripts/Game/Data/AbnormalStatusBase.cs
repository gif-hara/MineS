using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public abstract class AbnormalStatusBase<T, O>
		where T : struct
		where O : struct
	{
		public int RemainingTurn{ protected set; get; }

		public T Type{ protected set; get; }

		public abstract O OppositeType{ get; }

		public AbnormalStatusBase(int remainingTurn, T type)
		{
			this.RemainingTurn = remainingTurn;
			this.Type = type;
		}

		public void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.RemainingTurn--;
		}

		public bool IsValid
		{
			get
			{
				return this.RemainingTurn >= 0;
			}
		}
	}
}