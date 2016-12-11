using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
			private List<AudioClip> clips = new List<AudioClip>();

			public Element(Range floorRange)
				: base(floorRange)
			{
				
			}

			public AudioClip Clip
			{
				get
				{
					var playClips = this.clips.Where(c => c != BGMManager.Instance.Current).ToList();
					return playClips[Random.Range(0, playClips.Count)];
				}
			}

			public bool CanPlay(int floor)
			{
				return this.floorRange.min == floor;
			}
#if UNITY_EDITOR
			public void AddClip(string clipName)
			{
				this.clips.Add(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/BGM/{0}.mp3", clipName), typeof(AudioClip)) as AudioClip);
			}
#endif
		}

		[SerializeField]
		private List<Element> elements = new List<Element>();

		public AudioClip Get(int floor)
		{
			return this.elements.Find(e => e.IsMatch(floor)).Clip;
		}

		public bool CanPlay(int floor)
		{
			if(this.elements.Count <= 0)
			{
				return false;
			}

			return this.elements.Find(e => e.IsMatch(floor)).CanPlay(floor);
		}
#if UNITY_EDITOR
		
		public void Check(int floorMax)
		{
			new TableChecker().Check(this.elements, this.GetType(), floorMax);
		}

		public static BGMTable CreateFromCsv(string dungeonName)
		{
			var csv = CsvParser.Split(AssetDatabase.LoadAssetAtPath(string.Format("Assets/DataSources/Csv/Dungeon/{0}BGMTable.csv", dungeonName), typeof(TextAsset)) as TextAsset);
			var result = new BGMTable();
			foreach(var c in csv)
			{
				var floorRange = new Range(int.Parse(c[0]), int.Parse(c[1]));
				var element = result.elements.Find(e => e.IsMatchRange(floorRange));
				if(element == null)
				{
					element = new Element(floorRange);
					result.elements.Add(element);
				}
				element.AddClip(c[2]);
			}

			return result;

		}
#endif
	}
}