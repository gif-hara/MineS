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
	public class BGMTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField]
			private List<AudioClip> clips;

			public AudioClip Clip{ get { return this.clips[Random.Range(0, this.clips.Count)]; } }

			public bool CanPlay(int floor)
			{
				return this.floorRange.min == floor;
			}
		}

		[SerializeField]
		private List<Element> elements;

		public AudioClip Get(int floor)
		{
			return this.elements.Find(e => e.IsMatch(floor)).Clip;
		}

		public bool CanPlay(int floor)
		{
			return this.elements.Find(e => e.IsMatch(floor)).CanPlay(floor);
		}
	}
}