using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnPointerDownEvent : MonoBehaviour, IPointerDownHandler
	{
		[SerializeField]
		private UnityEvent otherEvent;

#region IPointerDownHandler implementation

		public void OnPointerDown(PointerEventData eventData)
		{
			this.otherEvent.Invoke();
		}

#endregion
	}
}