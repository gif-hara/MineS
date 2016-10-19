using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ChangeDungeonDataAction : CellClickActionBase
	{
		private DungeonDataBase dungeonData;

		public ChangeDungeonDataAction(DungeonDataBase dungeonData)
		{
			this.dungeonData = dungeonData;
		}

		public override void Invoke(CellData data)
		{
			DungeonManager.Instance.ChangeDungeonData(this.dungeonData);
		}

		public override void SetCellData(CellData data)
		{
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
	}
}