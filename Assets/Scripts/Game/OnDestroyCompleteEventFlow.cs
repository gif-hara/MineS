using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnDestroyCompleteEventFlow : MonoBehaviour
	{
		void OnDestroy()
		{
			EventFlowController.Instance.Complete();
		}
	}
}