using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class InvokeOtherDungeonProccess
	{
		[SerializeField]
		private List<OtherDungeonProccessBase> proccesses;

		public void Invoke()
		{
			this.proccesses.ForEach(p => p.Invoke());
		}
	}
}