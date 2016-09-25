using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class InvokeTrapActionBase : CellClickActionBase
	{
		public abstract Sprite Image{ get; }

		public override void OnUseXray()
		{
			this.cellController.SetImage(this.Image);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Trap;
			}
		}

	}
}