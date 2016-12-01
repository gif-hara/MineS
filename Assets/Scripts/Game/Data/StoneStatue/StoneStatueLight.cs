using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class StoneStatueLight : StoneStatue
	{
		public StoneStatueLight(CellData cellData)
			: base(GameDefine.StoneStatueType.Light, cellData)
		{
		}

		public override void StartUp()
		{
			base.StartUp();
			CellManager.Instance.AllActionFromLightStoneStatue();
		}
	}
}