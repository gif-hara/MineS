using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class BlankCell : CellData
	{
		public BlankCell(int y, int x) : base(y, x)
		{
		}

		protected override void InternalAction()
		{
			Debug.Log("Blank Cell Action!");
		}

		public override void InternalDescription()
		{
			Debug.Log("Blank Cell Description!");
		}
	}
}