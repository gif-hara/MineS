using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SEManager : SingletonMonoBehaviour<SEManager>
	{
		public List<SerializeFieldGetter.AudioClip> walks;

		[SerializeField]
		private SEElement prefabElement;

		private Dictionary<AudioClip, SEElement> dictionary = new Dictionary<AudioClip, SEElement>();

		public void PlaySE(AudioClip clip)
		{
			SEElement element;
			if(!this.dictionary.TryGetValue(clip, out element))
			{
				element = Instantiate(this.prefabElement, this.transform) as SEElement;
				element.name = clip.name;
				this.dictionary.Add(clip, element);
			}

			element.PlaySE(clip);
		}

		public void PlaySE(SerializeFieldGetter.AudioClip getter)
		{
			this.PlaySE(getter.Element);
		}
	}
}