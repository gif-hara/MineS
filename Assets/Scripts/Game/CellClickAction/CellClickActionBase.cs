﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class CellClickActionBase
	{
		public abstract GameDefine.EventType EventType{ get; }

		public abstract void Invoke(CellData data);
	}
}