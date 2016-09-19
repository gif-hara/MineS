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
	public class OnModifiedDescriptionDataSetTitle : MonoBehaviour, IReceiveModifiedDescriptionData
	{
		[SerializeField]
		private Text target;

#region IReceiveModifiedDescriptionData implementation

		public void OnModifiedDescriptionData(DescriptionData.Element data)
		{
			this.target.text = data.Title;
		}

		public void OnModifiedDescriptionData(CharacterData data)
		{
		}

#endregion
	}
}