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
		public CharacterData Holder{ protected set; get; }

		public int RemainingTurn{ protected set; get; }

		public int WaitTurn{ protected set; get; }

		public GameDefine.AbnormalStatusType Type{ protected set; get; }

		public GameDefine.AbnormalStatusType OppositeType{ protected set; get; }

		public AbnormalStatusBase(CharacterData holder, int remainingTurn, int waitTurn)
		{
			this.Holder = holder;
			this.RemainingTurn = remainingTurn;
			this.WaitTurn = waitTurn;
		}

		public virtual void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			if(this.WaitTurn > 0)
			{
				this.WaitTurn--;
				return;
			}

			this.RemainingTurn--;
			this.InternalTurnProgress(type, turnCount);
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

		protected virtual void InternalTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
		}
	}
}