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
	public abstract class AbnormalStatusBase
	{
		public int RemainingTurn{ protected set; get; }

		public GameDefine.AbnormalStatusType Type{ protected set; get; }

		public GameDefine.AbnormalStatusType OppositeType{ protected set; get; }

		public AbnormalStatusBase(int remainingTurn)
		{
			this.RemainingTurn = remainingTurn;
		}

		public virtual void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.RemainingTurn--;
		}

		public void AddRemainingTurn(int value)
		{
			this.RemainingTurn += value;
			this.RemainingTurn = this.RemainingTurn < 0 ? 0 : this.RemainingTurn;
		}

		public bool IsValid
		{
			get
			{
				return this.RemainingTurn > 0;
			}
		}
	}
}