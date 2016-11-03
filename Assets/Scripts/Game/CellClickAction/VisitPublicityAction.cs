﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class VisitPublicityAction : CellClickActionBase
	{
		public VisitPublicityAction()
		{
			
		}

		public override void Invoke(CellData data)
		{
			PublicityManager.Instance.OpenUI();
		}

		public override void SetCellController(CellController cellController)
		{
			cellController.SetImage(this.Image);
		}

		public override void SetCellData(CellData data)
		{
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData("Publicity"));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Publicity;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.publicity.Element;
			}
		}
	}
}