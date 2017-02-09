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
	public class OnModifiedCharacterDataSetGaugeArmor : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Gauge target;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
		    var value = data.ArmorMax <= 0 ? 0.0f : (float) data.Armor / data.ArmorMax;
		    value = value > 1.0f ? 1.0f : value;
		    this.target.Set(value);
		}

#endregion
	}
}