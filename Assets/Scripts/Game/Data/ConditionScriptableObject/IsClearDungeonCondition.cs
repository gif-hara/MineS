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
				Debug.LogFormat("type = {0} isClear = {1}", this.type, ProgressData.Instance.IsClearDungeon(this.type));
				return ProgressData.Instance.IsClearDungeon(this.type);
			}
		}
	}
}