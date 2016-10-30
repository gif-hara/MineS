using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class CellSerializeData
	{
		[SerializeField]
		public int mapChipId;

		[SerializeField]
		public bool canStep;

		[SerializeField]
		public bool isIdentification;

		[SerializeField]
		public int lockCount;
	}
}