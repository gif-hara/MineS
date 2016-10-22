using UnityEngine;
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
			if(DungeonManager.Instance.IsClear)
			{
				InformationManager.GameClear(DungeonManager.Instance.CurrentData.Name);
				ResultManager.Instance.Invoke(GameDefine.GameResultType.Clear, ResultManager.Instance.causeClear.Element.Get);
			}
			else
			{
				DungeonManager.Instance.NextFloorEvent(1);
			}
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Stair;
			}
		}

		public override Sprite Image
		{
			get
			{
				return null;
			}
		}
	}
}