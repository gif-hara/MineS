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
		private bool allIdentification;

		[SerializeField]
		private Cell initialStepPosition;

		[SerializeField]
		private List<CellCreator> creators;

		[SerializeField][Multiline(8)]
		private List<string> mapChip;

		public bool AllIdentification{ get { return this.allIdentification; } }

		public List<CellCreator> CellCreators{ get { return this.creators; } }

		public List<string> MapChip{ get { return this.mapChip; } }

		[ContextMenu("Check")]
		private void AssertionCheck()
		{
		}

		[ContextMenu("Apply Shop Data")]
		private void ApplyShopData()
		{
            this.shopTable = ShopTable.CreateFromCsv(this.name);
        }

		public override CellData[,] Create(CellManager cellManager)
		{
			return new FixDungeonCreator().Create(cellManager, this, GameDefine.CellRowMax, GameDefine.CellCulumnMax);
		}

		public override void InitialStep()
		{
			var initialCell = CellManager.Instance.CellDatabase[this.initialStepPosition.y, this.initialStepPosition.x];
			var isXray = PlayerManager.Instance.Data.IsXray;
			initialCell.Steppable(isXray);
			initialCell.Identification(true, isXray, false);
		}
	}
}