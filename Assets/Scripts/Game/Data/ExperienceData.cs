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
			if(!this.CanLevelUp(level))
			{
				return -1;
			}

			return this.experiences[level];
		}

		public bool CanLevelUp(int level)
		{
			return level < this.experiences.Count - 1;
		}

#if UNITY_EDITOR
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

		[ContextMenu("From Csv")]
		private void FromCsv()
		{
			var splitData = CsvParser.Split(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Csv/PlayerGrowthData.csv", typeof(TextAsset)) as TextAsset);
			this.experiences = new List<int>();
			foreach(var s in splitData)
			{
				this.experiences.Add(int.Parse(s[3]));
			}
		}
#endif
	}
}