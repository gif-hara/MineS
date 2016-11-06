using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedOptionDataSetSliderSEVolume : MonoBehaviour, IReceiveModifiedOptionData
	{
		[SerializeField]
		private Slider target;

#region IReceiveModifiedOptionData implementation

		public void OnModifiedOptionData(OptionData data)
		{
			this.target.value = data.SEVolume;
		}

#endregion
	}
}