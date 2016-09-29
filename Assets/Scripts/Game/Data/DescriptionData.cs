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
			public string Key{ get { return this.key; } }

			[SerializeField]
			private string key;

			public string Title{ get { return this.title.ToString(); } }

			[SerializeField]
			private StringAsset.Finder title;

			public string Message{ get { return this.message.ToString(); } }

			[SerializeField]
			private StringAsset.Finder message;

			public Sprite Image{ get { return this.image; } }

			[SerializeField]
			private Sprite image;
		}

		[SerializeField]
		private List<Element> database;

		private static Dictionary<string, Element> finder = null;

		public Element Get(string key)
		{
			if(finder == null)
			{
				finder = new Dictionary<string, Element>();
				for(int i = 0; i < database.Count; i++)
				{
					var data = database[i];
					finder.Add(data.Key, data);
				}
			}

			return finder[key];
		}
	}
}