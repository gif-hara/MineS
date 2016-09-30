using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DeployDescriptionOnItem : DeployDescriptionBase
	{
		private Item item;

		public DeployDescriptionOnItem(Item item)
		{
			this.item = item;
		}

		public override void Deploy()
		{
			if(this.item == null)
			{
				return;
			}
			DescriptionManager.Instance.Deploy(this.item);
		}
	}
}