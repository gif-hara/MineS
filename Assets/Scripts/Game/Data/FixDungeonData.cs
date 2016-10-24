using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class FixDungeonData : DungeonDataBase
	{
		[System.Serializable]
		public class CellCreator
		{
			[SerializeField]
			private int x;

			[SerializeField]
			private int y;

			[SerializeField]
			FixDungeonCellCreatorBase creator;

			public int X{ get { return this.x; } }

			public int Y{ get { return this.y; } }

			public CellData Create(CellController cellController, MapChipCreatorBase mapChipCreator)
			{
				return this.creator.Create(this.y, this.x, cellController, mapChipCreator);
			}
		}

		[SerializeField]
		private List<CellCreator> creators;

		[SerializeField][Multiline(8)]
		private string mapChip;

		public List<CellCreator> CellCreators{ get { return this.creators; } }

		public string MapChip{ get { return this.mapChip; } }

		[ContextMenu("Check")]
		private void AssertionCheck()
		{
		}

		public override CellData[,] Create(CellManager cellManager)
		{
			return new FixDungeonCreator().Create(cellManager, this, GameDefine.CellRowMax, GameDefine.CellCulumnMax);
		}
	}
}