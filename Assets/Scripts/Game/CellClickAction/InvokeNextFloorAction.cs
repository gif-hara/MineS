﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeNextFloorAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			DungeonManager.Instance.NextFloorEvent();
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Stair;
			}
		}
	}
}