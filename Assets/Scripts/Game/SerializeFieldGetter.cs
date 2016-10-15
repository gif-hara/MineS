using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public abstract class SerializeFieldGetter
	{
		[System.Serializable]
		public abstract class Base<T>
		{
			[SerializeField]
			private T element;

			public T Element{ get { return this.element; } }

			protected Base()
			{
			}
		}

		[System.Serializable]
		public class Sprite : Base<UnityEngine.Sprite>
		{
		}

		[System.Serializable]
		public class Int : Base<int>
		{
		}

		[System.Serializable]
		public class StringAssetFinder : Base<StringAsset.Finder>
		{
		}

		[System.Serializable]
		public class GameObject : Base<UnityEngine.GameObject>
		{
		}

	}
}