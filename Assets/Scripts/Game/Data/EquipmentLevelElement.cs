using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class EquipmentLevelElement : ScriptableObject
	{
		[SerializeField]
		private float level1;

		[SerializeField]
		private float level2;

		[SerializeField]
		private float level3;

		public float Get(int level)
		{
			return this.ToArray[level];
		}

		private float[] ToArray
		{
			get
			{
				return new float[GameDefine.EquipmentLevelMax]{ this.level1, this.level2, this.level3 };
			}
		}
	}
}