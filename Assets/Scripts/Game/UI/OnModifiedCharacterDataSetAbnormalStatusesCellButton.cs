using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedCharacterDataSetAbnormalStatusesCellButton : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private CellController cellControllerPrefab;

		[SerializeField]
		private RectTransform buffParent;

		[SerializeField]
		private RectTransform debuffParent;

		private List<AbnormalStatusBase> oldAbnormalStatuses = new List<AbnormalStatusBase>();

		private Dictionary<GameDefine.AbnormalStatusType, CellController> buffCells = new Dictionary<GameDefine.AbnormalStatusType, CellController>();

		private Dictionary<GameDefine.AbnormalStatusType, CellController> debuffCells = new Dictionary<GameDefine.AbnormalStatusType, CellController>();

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			var addedAbnormalStatuses = data.AbnormalStatuses.FindAll(a => this.oldAbnormalStatuses.Find(o => o.Type == a.Type) == null);
			addedAbnormalStatuses.ForEach(a =>
			{
				var isBuff = GameDefine.IsBuff(a.Type);
				var dictionary = isBuff ? buffCells : debuffCells;
				var parent = isBuff ? this.buffParent : this.debuffParent;
				var cellController = Instantiate(this.cellControllerPrefab, parent, false) as CellController;
				cellController.SetActiveStatusObject(false);
				var cellData = new CellData(cellController);
				cellData.BindDeployDescription(new DeployDescriptionOnDescriptionData(GameDefine.GetAbnormalStatusDescriptionKey(a.Type)));
				cellController.SetCellData(cellData);
				cellController.SetImage(TextureManager.Instance.abnormalStatus.GetIcon(a.Type));
				dictionary.Add(a.Type, cellController);
			});

			var removedAbnormalStatuses = this.oldAbnormalStatuses.FindAll(o => data.AbnormalStatuses.Find(a => a.Type == o.Type) == null);
			removedAbnormalStatuses.ForEach(r =>
			{
				var isBuff = GameDefine.IsBuff(r.Type);
				var dictionary = isBuff ? buffCells : debuffCells;
				Destroy(dictionary[r.Type].gameObject);
				dictionary.Remove(r.Type);
			});

			this.oldAbnormalStatuses = new List<AbnormalStatusBase>(data.AbnormalStatuses);
		}

#endregion
	}
}