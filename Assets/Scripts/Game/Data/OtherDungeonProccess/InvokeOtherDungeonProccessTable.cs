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
	public class InvokeOtherDungeonProccessTable
	{
		[SerializeField]
		private int invokeFloor;

		[SerializeField]
		private InvokeOtherDungeonProccess invoker;


		public bool CanInvoke(int floor)
		{
			return this.invokeFloor == floor;
		}

		public void Invoke()
		{
			this.invoker.Invoke();
		}
	}
}