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
	public class OnModifiedPlayerDataSetTextLevel : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			var playerData = data as PlayerData;
			Debug.AssertFormat(playerData != null, "PlayerDataにキャストできませんでした.");

			this.target.text = this.format.Format(playerData.Level);
		}

#endregion
	}
}