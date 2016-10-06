using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.EventSystems;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnClickSelectBlackSmithMode : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private GameDefine.InventoryModeType openType;

		[SerializeField]
		private StringAsset.Finder openMessage;

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			var playerManager = PlayerManager.Instance;
			if(!playerManager.Data.Inventory.IsPossessionEquipment)
			{
				InformationManager.OnNotPossessionEquipment();
				return;
			}

			InformationManager.AddMessage(this.openMessage.Get);
			PlayerManager.Instance.OpenInventoryUI(this.openType);
		}

#endregion
	}
}