using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class IsClearDungeonCondition : ConditionScriptableObjectBase
	{
		[SerializeField]
		private GameDefine.DungeonType type;

		public override bool Condition
		{
			get
			{
				return ProgressData.Instance.IsClearDungeon(this.type);
			}
		}
	}
}