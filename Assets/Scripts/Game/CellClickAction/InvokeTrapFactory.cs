using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public static class InvokeTrapFactory
	{
		public static InvokeTrapActionBase Create(GameDefine.TrapType type)
		{
			if(GameDefine.IsAbnormalTrap(type))
			{
				return new InvokeAbnormalStatusTrapAction(type);
			}
			else if(type == GameDefine.TrapType.Mine)
			{
				return new InvokeMineTrapAction();
			}

			Debug.AssertFormat(false, "未実装の罠です. type = {0}", type);
			return null;
		}
	}
}