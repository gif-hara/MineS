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
	public class BGMData : ScriptableObject
	{
		[SerializeField]
		public AudioClip clip;

		[SerializeField]
		public float startLoop;

		[SerializeField]
		public float endLoop;

		public AudioClip Clip{ get { return this.clip; } }

		public float StartLoop{ get { return this.startLoop; } }

		public float EndLoop{ get { return this.endLoop; } }
	}
}