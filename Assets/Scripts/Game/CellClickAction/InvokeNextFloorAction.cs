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
				DungeonManager.Instance.ClearDungeon(GameDefine.GameResultType.Clear);
			}
			else
			{
				DungeonManager.Instance.NextFloorEvent(1, false);
			}
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			this.cellController.SetImage(this.Image);
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
				return TextureManager.Instance.stairImage.Element;
			}
		}

		public override void Serialize(int y, int x)
		{
		}

		public override void Deserialize(int y, int x)
		{
		}
	}
}