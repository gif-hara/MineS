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

			public string Title
			{
				get
				{
					if(string.IsNullOrEmpty(this._title))
					{
						this._title = this.title.ToString();
					}
					return this._title;
				}
			}

			[SerializeField]
			private StringAsset.Finder title;

			private string _title = null;

			public string Message
			{
				get
				{
					if(string.IsNullOrEmpty(this._message))
					{
						this._message = this.message.ToString();
					}

					return this._message;
				}
			}

			[SerializeField]
			private StringAsset.Finder message;

			private string _message;

			public Sprite Image{ get { return this.image; } }

			[SerializeField]
			private Sprite image;

			public Element(StringAsset.Finder title, StringAsset.Finder message, Sprite image)
			{
				this.title = title;
				this._title = this.title.ToString();
				this.message = message;
				this._message = this.message.ToString();
				this.image = image;
			}

			public Element(string title, string message, Sprite image)
			{
				this._title = title;
				this._message = message;
				this.image = image;
			}
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