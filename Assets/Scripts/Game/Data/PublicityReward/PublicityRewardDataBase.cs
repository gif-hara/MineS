using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class PublicityRewardDataBase : ScriptableObject
	{
		public abstract bool Grant();
	}
}