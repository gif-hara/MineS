using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SelectBlackSmithRemoveAbilitySelectAbilityAction : CellClickActionBase
	{
		private int index;

		public SelectBlackSmithRemoveAbilitySelectAbilityAction(int index)
		{
			this.index = index;
		}

		public override void Invoke(CellData data)
		{
			BlackSmithManager.Instance.InvokeRemoveAbility(this.index);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Item;
			}
		}

		public override Sprite Image
		{
			get
			{
				return null;
			}
		}
	}
}