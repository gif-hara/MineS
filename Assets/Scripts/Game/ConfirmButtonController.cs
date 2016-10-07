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

		public void Initialize(string message, UnityAction action)
		{
			this.text.text = message;
			this.action = action;
		}

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			if(this.action != null)
			{
				this.action.Invoke();
			}
			ConfirmManager.Instance.Close();
		}

#endregion
	}
}