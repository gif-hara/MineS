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
	public class OnModifiedCharacterDataSetGaugeMagicPoint : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Gauge target;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			this.target.Set((float)data.MagicPoint / data.MagicPointMax);
		}

#endregion
	}
}