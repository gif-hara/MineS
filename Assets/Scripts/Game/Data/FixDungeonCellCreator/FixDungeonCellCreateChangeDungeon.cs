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
	public class FixDungeonCellCreateChangeDungeon : FixDungeonCellCreatorBase
	{
		[SerializeField]
		private ConditionScriptableObjectBase conditioner;

		[SerializeField]
		private DungeonDataBase data;

		[SerializeField]
		private string descriptionKey;

		public override CellData Create(int y, int x, CellController cellController)
		{
			var cellData = new CellData(y, x, 0, cellController);

			if(this.conditioner.Condition)
			{
				cellData.BindCellClickAction(new ChangeDungeonDataAction(this.data, this.descriptionKey));
			}

			return cellData;
		}
	}
}