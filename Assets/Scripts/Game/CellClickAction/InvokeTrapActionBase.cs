using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class InvokeTrapActionBase : CellClickActionBase
	{
		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Trap;
			}
		}

	}
}