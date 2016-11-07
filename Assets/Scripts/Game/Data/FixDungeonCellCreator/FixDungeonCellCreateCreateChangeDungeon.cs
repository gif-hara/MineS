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
	public class FixDungeonCellCreateCreateChangeDungeon : FixDungeonCellCreatorBase
	{
		[SerializeField]
		private ConditionScriptableObjectBase conditioner;

		[SerializeField]
		private DungeonDataBase data;

		[SerializeField]
		private string descriptionKey;

		public override CellData Create(int y, int x, CellController cellController, MapChipCreatorBase mapChipCreator)
		{
			var cellData = new CellData(y, x, mapChipCreator.Get(y, x), cellController);

			if(this.conditioner.Condition)
			{
				cellData.BindCellClickAction(new CreateChangeDungeonDataAction(this.data, this.descriptionKey));
			}

			return cellData;
		}
	}
}