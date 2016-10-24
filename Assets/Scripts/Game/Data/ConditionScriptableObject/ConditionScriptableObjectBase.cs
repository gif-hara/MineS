using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class ConditionScriptableObjectBase : ScriptableObject
	{
		public abstract bool Condition{ get; }
	}
}