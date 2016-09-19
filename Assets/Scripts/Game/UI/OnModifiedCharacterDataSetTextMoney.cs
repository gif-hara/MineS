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
	public class OnModifiedCharacterDataSetTextMoney : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			this.target.text = this.format.Format(data.Money);
		}

#endregion
	}
}