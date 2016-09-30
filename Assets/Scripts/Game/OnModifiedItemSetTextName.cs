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
	public class OnModifiedItemSetTextName : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
			this.target.text = this.format.Format(item.InstanceData.ItemName);
		}

#endregion
	}
}