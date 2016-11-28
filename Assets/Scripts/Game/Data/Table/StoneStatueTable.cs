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
	public class StoneStatueTable
	{
		[System.Serializable]
		public class Element : TableElementBase
		{
			[SerializeField]
			private List<CreateTable> createTable;

			[SerializeField][Range(0, 100)]
			private int probability;

			public int Probability{ get { return this.probability; } }

			public Element(Range floorRange)
				: base(floorRange)
			{
				
			}

			public GameDefine.StoneStatueType GetCreateType()
			{
				return this.createTable[GameDefine.Lottery(this.createTable)].Type;
			}

			public bool CanCreate
			{
				get
				{
					return this.probability > Random.Range(0, 100);
				}
			}
		}

		[System.Serializable]
		public class CreateTable : IProbability
		{
			[SerializeField]
			private GameDefine.StoneStatueType type;

			[SerializeField]
			private int probability;

			public GameDefine.StoneStatueType Type{ get { return this.type; } }

			public int Probability{ get { return this.probability; } }

#if UNITY_EDITOR
			public static CreateTable Create(GameDefine.StoneStatueType type, int probability)
			{
				var result = new CreateTable();
				result.type = type;
				result.probability = probability;

				return result;
			}
#endif
		}

		[SerializeField]
		private List<Element> elements = new List<Element>();

		public bool CanCreate(int floor)
		{
			var element = this.elements.Find(e => e.IsMatch(floor));
			if(element == null)
			{
				return false;
			}

			return element.CanCreate;
		}

		public GameDefine.StoneStatueType GetCreateType(int floor)
		{
			var element = this.elements.Find(e => e.IsMatch(floor));
			return element.GetCreateType();
		}

	}
}