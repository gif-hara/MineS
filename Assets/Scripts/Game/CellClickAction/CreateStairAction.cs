using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateStairAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetImage(DungeonManager.Instance.CurrentData.StairImage);
			data.BindCellClickAction(new InvokeNextFloorAction());
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("NextFloor"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Stair;
			}
		}
	}
}