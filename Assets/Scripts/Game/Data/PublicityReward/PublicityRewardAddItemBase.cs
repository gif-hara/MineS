using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class PublicityRewardAddItemBase : PublicityRewardDataBase
	{
		public override bool Grant()
		{
			if(!PlayerManager.Instance.Data.Inventory.IsFreeSpace)
			{
				PublicityManager.Instance.StartFullInventoryTalk();
				return false;
			}

			this.InternalGrant();
			return true;
		}

		protected abstract void InternalGrant();
	}
}