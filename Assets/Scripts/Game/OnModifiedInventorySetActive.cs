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
	public class OnModifiedInventorySetActive : MonoBehaviour, IReceiveModifiedInventory
	{
		[SerializeField]
		private List<GameObject> targets;

		[SerializeField]
		private bool isActive;

#region IReceiveModifiedInventory implementation

		public void OnModifiedInventory(Inventory data)
		{
			this.targets.ForEach(g => g.SetActive(this.isActive));
		}

#endregion
	}
}