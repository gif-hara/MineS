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
	public class OnModifiedCharacterDataSetImage : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Image target;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			this.target.sprite = data.Image;
		}

#endregion
	}
}