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
	public class OnModifiedDescriptionDataSetImage : MonoBehaviour, IReceiveModifiedDescriptionData
	{
		[SerializeField]
		private Image target;

#region IReceiveModifiedDescriptionData implementation

		public void OnModifiedDescriptionData(DescriptionData.Element data)
		{
			this.target.sprite = data.Image;
		}

		public void OnModifiedDescriptionData(CharacterData data)
		{
			this.target.sprite = data.Image;
		}

#endregion
	}
}