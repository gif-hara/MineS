using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class IsVisitBlackSmithCondition : ConditionScriptableObjectBase
	{
		public override bool Condition
		{
			get
			{
				return MineS.SaveData.Progress.VisitBlackSmithCount > 0;
			}
		}
	}
}