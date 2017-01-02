﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedOptionDataSetToggleVisiblePlayTime : MonoBehaviour, IReceiveModifiedOptionData
	{
		[SerializeField]
		private Toggle target;

#region IReceiveModifiedOptionData implementation

		public void OnModifiedOptionData(OptionData data)
		{
			this.target.isOn = data.VisiblePlayTime;
		}

#endregion
	}
}