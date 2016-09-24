using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedDebugSetTextAbnormalStatusRemainingTurn : MonoBehaviour
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

		void Start()
		{
			DebugManager.Instance.AddAbnormalStatusRemainingTurnEvent(this.OnModifiedData);
		}

		private void OnModifiedData(int value)
		{
			this.target.text = this.format.Format(value);
		}
	}
}