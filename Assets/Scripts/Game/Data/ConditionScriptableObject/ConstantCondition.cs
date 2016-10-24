using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ConstantCondition : ConditionScriptableObjectBase
	{
		public override bool Condition
		{
			get
			{
				return true;
			}
		}
	}
}