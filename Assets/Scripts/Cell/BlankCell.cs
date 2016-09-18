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
		public override void Action()
		{
			Debug.Log("Blank Cell");
		}

		public override void Description()
		{
		}
	}
}