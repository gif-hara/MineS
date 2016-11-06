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

		public SerializeFieldGetter.AudioClip stair;

		public SerializeFieldGetter.AudioClip recovery;

		public SerializeFieldGetter.AudioClip slash;

		public SerializeFieldGetter.AudioClip damage;

		public SerializeFieldGetter.AudioClip dead;

		public SerializeFieldGetter.AudioClip equipOn;

		public SerializeFieldGetter.AudioClip equipOff;

		public SerializeFieldGetter.AudioClip acquireItem;

		public SerializeFieldGetter.AudioClip acquireMoney;

		public SerializeFieldGetter.AudioClip blackSmith;

		public SerializeFieldGetter.AudioClip levelUp;

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
				element.SetVolume(MineS.SaveData.Option.SEVolume);
				this.dictionary.Add(clip, element);
			}

			element.PlaySE(clip);
		}

		public void PlaySE(SerializeFieldGetter.AudioClip getter)
		{
			this.PlaySE(getter.Element);
		}

		public void SetVolume(float value)
		{
			foreach(var s in this.dictionary)
			{
				s.Value.SetVolume(value);
			}
		}
	}
}