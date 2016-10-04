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
	public class OnModifiedItemSetImage : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private Image target;

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
            this.target.sprite = item.InstanceData.Image;
        }

#endregion
	}
}