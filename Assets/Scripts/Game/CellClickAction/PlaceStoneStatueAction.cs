using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class PlaceStoneStatueAction : CellClickActionBase
	{
		[SerializeField]
		private GameDefine.StoneStatueType type;

		public PlaceStoneStatueAction()
		{
		}

		public PlaceStoneStatueAction(GameDefine.StoneStatueType type)
		{
			this.type = type;
		}

		public override void Invoke(CellData data)
		{
		}

		public override void OnIdentification(CellData cellData)
		{
			base.OnIdentification(cellData);
			this.cellController.SetImage(this.Image);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.StoneStatue;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.stoneStatueImage.Element;
			}
		}

		public override void Serialize(int y, int x)
		{
		}

		public override void Deserialize(int y, int x)
		{
		}

		public override void LateDeserialize(int y, int x)
		{
			if(this.cellController.Data.IsIdentification)
			{
				this.cellController.SetImage(this.Image);
			}
		}
	}
}