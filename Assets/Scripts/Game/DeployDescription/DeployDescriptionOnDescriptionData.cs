using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DeployDescriptionOnDescriptionData : DeployDescriptionBase
	{
		private string key;

		public DeployDescriptionOnDescriptionData(string key)
		{
			this.key = key;
		}

		public override void Deploy()
		{
			DescriptionManager.Instance.Deploy(this.key);
		}
	}
}