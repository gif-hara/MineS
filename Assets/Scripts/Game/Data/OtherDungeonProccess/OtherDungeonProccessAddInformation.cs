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
	public class OtherDungeonProccessAddInformation : OtherDungeonProccessBase
	{
		[SerializeField]
		StringAsset.Finder message;

		public override void Invoke()
		{
			InformationManager.AddMessage(this.message);
		}
	}
}