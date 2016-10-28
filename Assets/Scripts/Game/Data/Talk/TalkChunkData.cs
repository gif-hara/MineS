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
	public class TalkChunkData : ScriptableObject
	{
		[SerializeField]
		private List<TalkDataBase> elements;

		public TalkDataBase Get(int index)
		{
			if(index >= this.elements.Count)
			{
				return null;
			}

			return this.elements[index];
		}
	}
}