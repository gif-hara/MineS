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
	public class OnModifiedCharacterDataSetTextHitPoint : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

		[SerializeField]
		private Color basicColor;

		[SerializeField]
		private Color limitOverColor;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			this.target.text = this.format.Format(data.HitPoint, data.HitPointMax);
			this.target.color = data.HitPoint > data.HitPointMax ? this.limitOverColor : this.basicColor;
		}

#endregion
	}
}