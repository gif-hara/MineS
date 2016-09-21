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
	public abstract class AbnormalStatus
	{
		public int RemainingTurn{ protected set; get; }

		public GameDefine.AbnormalStatusType Type{ protected set; get; }

		public GameDefine.AbnormalStatusType OppositeType{ protected set; get; }

		public AbnormalStatus(int remainingTurn)
		{
			this.RemainingTurn = remainingTurn;
		}

		public virtual void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.RemainingTurn--;
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