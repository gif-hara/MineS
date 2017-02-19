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
		public enum BadConditionType:int
		{
			None,

			CloseStair,
		}

		[SerializeField]
		private ConditionScriptableObjectBase conditioner;

		[SerializeField]
		private BadConditionType badConditionType;

		[SerializeField]
		private DungeonDataBase data;

		[SerializeField]
		private string descriptionKey;

		[SerializeField]
		private ConditionScriptableObjectBase canChange;

        [SerializeField]
        private Sprite stairImage;

        [SerializeField]
		private bool debugOnly;

		public override CellData Create(int y, int x, CellController cellController, MapChipCreatorBase mapChipCreator)
		{
			var cellData = new CellData(y, x, mapChipCreator.Get(y, x), cellController);

#if RELEASE
			if(this.debugOnly)
			{
				return cellData;
			}
#endif

			if(this.conditioner.Condition)
			{
				cellData.BindCellClickAction(new ChangeDungeonDataAction(this.data, this.descriptionKey, this.canChange, this.stairImage));
			}
			else
			{
				if(this.badConditionType == BadConditionType.CloseStair)
				{
					cellData.BindCellClickAction(new CloseStairAction());
				}
			}

			return cellData;
		}
	}
}