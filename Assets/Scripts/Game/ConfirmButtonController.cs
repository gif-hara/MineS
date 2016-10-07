using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ConfirmButtonController : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private Text text;

		private UnityAction action;

		private bool closeConfirmUI;

		public void Initialize(string message, UnityAction action, bool closeConfirmUI)
		{
			this.text.text = message;
			this.action = action;
			this.closeConfirmUI = closeConfirmUI;
		}

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			if(this.action != null)
			{
				this.action.Invoke();
			}

			if(this.closeConfirmUI)
			{
				ConfirmManager.Instance.Close();
			}
		}

#endregion
	}
}