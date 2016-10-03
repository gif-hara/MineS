using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SystemManager : MonoBehaviour
	{
		void Awake()
		{
			QualitySettings.vSyncCount = 1;
			Application.targetFrameRate = 60;
		}
	}
}