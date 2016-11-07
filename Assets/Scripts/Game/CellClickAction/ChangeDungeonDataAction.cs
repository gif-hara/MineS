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

		private string descriptionKey;

		private ConditionScriptableObjectBase canChange;

		public ChangeDungeonDataAction(DungeonDataBase dungeonData, string descriptionKey, ConditionScriptableObjectBase canChange)
		{
			this.dungeonData = dungeonData;
			this.descriptionKey = descriptionKey;
			this.canChange = canChange;
		}

		public override void Invoke(CellData data)
		{
			if(!this.canChange.Condition)
			{
				DescriptionManager.Instance.DeployEmergency("NotChangeDungeon");
				return;
			}

			ConfirmManager.Instance.Add(ConfirmManager.Instance.proceed, () =>
			{
				DungeonManager.Instance.ChangeDungeonData(this.dungeonData, false);
			}, true);
			ConfirmManager.Instance.Add(ConfirmManager.Instance.cancel, () =>
			{
			}, true);
			InformationManager.ConfirmEnterDungeon(this.dungeonData.Name);
		}

		public override void OnIdentification(CellData cellData)
		{
			base.OnIdentification(cellData);

		}

		public override void SetCellData(CellData data)
		{
			this.cellController.SetImage(this.Image);
			data.BindDeployDescription(new DeployDescriptionOnDescriptionData(this.descriptionKey));
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