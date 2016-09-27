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
	public class OnDeselectEvent : MonoBehaviour, IBeginDragHandler
	{
		[SerializeField]
		private UnityEvent otherEvent;

#region IBeginDragHandler implementation

		public void OnBeginDrag(PointerEventData eventData)
		{
			Debug.Log("OnBeginDrag");
		}

#endregion
	}
}