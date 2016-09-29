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
	public class OnModifiedCharacterDataSetName : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Text target;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			this.target.text = data.Name;
		}

#endregion
	}
}