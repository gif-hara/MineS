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
	[System.Serializable][CreateAssetMenu()]
	public class DescriptionData : ScriptableObject
	{
		[System.Serializable]
		public class Element
		{
			[SerializeField]
			private StringAsset.Finder title;

			[SerializeField]
			private StringAsset.Finder message;

			[SerializeField]
			private Image image;
		}

		[SerializeField]
		private List<Element> database;
	}
}