using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SEElement : MonoBehaviour
	{
		[SerializeField]
		private AudioSource source;

		public void PlaySE(AudioClip clip)
		{
			this.source.clip = clip;
			this.source.time = 0.0f;
			this.source.Play();
		}
	}
}