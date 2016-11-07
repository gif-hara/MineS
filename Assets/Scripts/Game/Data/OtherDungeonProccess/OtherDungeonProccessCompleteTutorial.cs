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
	public class OtherDungeonProccessCompleteTutorial : OtherDungeonProccessBase
	{
		public override void Invoke()
		{
			MineS.SaveData.Progress.CompleteTutorial();
		}
	}
}