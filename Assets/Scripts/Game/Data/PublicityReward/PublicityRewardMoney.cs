using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class PublicityRewardMoney : PublicityRewardDataBase
	{
		[SerializeField]
		private Range money;

		public override bool Grant()
		{
			var value = this.money.Random;
			PlayerManager.Instance.AddMoney(value, true);
			InformationManager.AcquireMoney(value);

			return true;
		}
	}
}