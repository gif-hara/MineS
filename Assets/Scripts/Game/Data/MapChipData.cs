using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class MapChipData : ScriptableObject
	{
		[SerializeField]
		private Sprite all;
		[SerializeField]
		private Sprite horizontal;
		[SerializeField]
		private Sprite vertical;
		[SerializeField]
		private Sprite leftBottom;
		[SerializeField]
		private Sprite leftRightBottom;
		[SerializeField]
		private Sprite leftRightTop;
		[SerializeField]
		private Sprite leftRightTopBottom;
		[SerializeField]
		private Sprite leftTop;
		[SerializeField]
		private Sprite leftTopBottom;
		[SerializeField]
		private Sprite rightBottom;
		[SerializeField]
		private Sprite rightTop;
		[SerializeField]
		private Sprite rightTopBottom;

		private Dictionary<int, Sprite> dictionary = null;

		private const int Left = (1 << 1);
		private const int Right = (1 << 2);
		private const int Top = (1 << 3);
		private const int Bottom = (1 << 4);

		public Sprite Get(int myId, int leftId, int rightId, int topId, int bottomId)
		{
			if(this.dictionary == null)
			{
				this.Initialize();
			}
			if(myId == 0)
			{
				return this.all;
			}

			int key = Left * leftId + Right * rightId + Top * topId + Bottom * bottomId;
			if(!this.dictionary.ContainsKey(key))
			{
				Debug.LogErrorFormat("key[{0}]のデータがありません. left = {1} right = {2} top = {3} bottom = {4}", key, leftId, rightId, topId, bottomId);
				return null;
			}

			return this.dictionary[key];
		}

		private void Initialize()
		{
			this.dictionary = new Dictionary<int, Sprite>();
			this.dictionary.Add(Left + Right, this.horizontal);
			this.dictionary.Add(Top + Bottom, this.vertical);
			this.dictionary.Add(Left + Bottom, this.leftBottom);
			this.dictionary.Add(Left + Right + Bottom, this.leftRightBottom);
			this.dictionary.Add(Left + Right + Top, this.leftRightTop);
			this.dictionary.Add(Left + Right + Top + Bottom, this.leftRightTopBottom);
			this.dictionary.Add(Left + Top, this.leftTop);
			this.dictionary.Add(Left + Top + Bottom, this.leftTopBottom);
			this.dictionary.Add(Right + Bottom, this.rightBottom);
			this.dictionary.Add(Right + Top, this.rightTop);
			this.dictionary.Add(Right + Top + Bottom, this.rightTopBottom);
		}
	}
}