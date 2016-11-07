using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class IsPossessionAnyItem : ConditionScriptableObjectBase
	{
		public override bool Condition
		{
			get
			{
				return PlayerManager.Instance.Data.Inventory.IsPossessionAny && PlayerManager.Instance.Data.Money <= 0;
			}
		}
	}
}