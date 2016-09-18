using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ButtonEvent : MonoBehaviour
	{
		[SerializeField]
		private CellController cellController;

		public void OnButtonDown()
		{
		}

		public void OnButtonUp()
		{
			this.cellController.Action();
		}
	}
}