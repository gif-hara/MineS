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
	public class OnClickOpenInventoryUI : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private GameDefine.InventoryModeType openType;

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			PlayerManager.Instance.OpenInventoryUI(this.openType);
		}

#endregion
	}
}