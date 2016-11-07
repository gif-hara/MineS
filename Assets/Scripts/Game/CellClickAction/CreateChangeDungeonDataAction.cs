using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateChangeDungeonDataAction : CellClickActionBase
	{
		private DungeonDataBase dungeonData;

		private string descriptionKey;

		private ConditionScriptableObjectBase canChange;

		public CreateChangeDungeonDataAction(DungeonDataBase dungeonData, string descriptionKey, ConditionScriptableObjectBase canChange)
		{
			this.dungeonData = dungeonData;
			this.descriptionKey = descriptionKey;
			this.canChange = canChange;
		}

		public override void Invoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			data.BindCellClickAction(new ChangeDungeonDataAction(this.dungeonData, this.descriptionKey, this.canChange));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Anvil;
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