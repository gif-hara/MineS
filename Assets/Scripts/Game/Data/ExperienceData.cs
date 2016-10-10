using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class ExperienceData : ScriptableObject
	{
		[SerializeField]
		private List<int> experiences;

		public List<int> Experiences{ get { return this.experiences; } }

		public int NeedNextLevel(int level)
		{
			if(level >= (this.experiences.Count - 1))
			{
				return -1;
			}

			return this.experiences[level + 1];
		}

		[ContextMenu("Algorithm 0")]
		private void Alrorithm0()
		{
			this.experiences = new List<int>();
			for(int i = 0; i < 100; i++)
			{
				this.experiences.Add((i * 2) + (i * i) + ((i * i) / 3));
			}
		}

		[ContextMenu("Algorithm 1")]
		private void Alrorithm1()
		{
			this.experiences = new List<int>();
			for(int i = 0; i < 100; i++)
			{
				this.experiences.Add(Mathf.FloorToInt((i * i * i) + (i * i)));
			}
		}
	}
}