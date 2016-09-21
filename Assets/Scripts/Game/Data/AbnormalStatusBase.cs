using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
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
	}
}