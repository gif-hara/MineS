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
	public class EnemyTable
	{
		[System.Serializable]
		public class Element
		{
			[SerializeField]
			private int floorMin;

			[SerializeField]
			private int floorMax;

			[SerializeField]
			private List<EnemyCreateTable> enemies;

			public bool IsMatchFloor(int floor)
			{
				return floor >= this.floorMin && floor <= this.floorMax;
			}

			public EnemyData Create()
			{
				var result = new EnemyData();
				var index = GameDefine.Lottery(this.enemies);
				result.Initialize(this.enemies[index].MasterData);
				return result;
			}
		}

		[SerializeField]
		private List<Element> elements;

		public EnemyData Create(int floor)
		{
#if DEBUG
			Debug.AssertFormat(this.elements.FindAll(e => e.IsMatchFloor(floor)).Count == 1, "敵テーブルが無い、または複数ありました.");
#endif
			return this.elements.Find(e => e.IsMatchFloor(floor)).Create();
		}

		public void Check(int floorMax)
		{
			var result = true;
			int floor = 1;
			Element element = this.elements.Find(e => e.IsMatchFloor(floor));
			while(element != null || floor < floorMax)
			{
				var existCount = this.elements.FindAll(e => e.IsMatchFloor(floor)).Count;
				result = result && existCount == 1;
				if(existCount == 0)
				{
					Debug.AssertFormat(false, "floor = {0}の敵テーブルがありません.", floor);
				}
				else if(existCount > 1)
				{
					Debug.AssertFormat(false, "floor = {0}に敵テーブルが複数存在しています.", floor);
				}
				floor++;
				element = this.elements.Find(e => e.IsMatchFloor(floor));
			}
			floor--;

			if(floor != floorMax)
			{
				Debug.AssertFormat(false, "階数の最大値と一致していません. floor = {0} max = {1}", floor, floorMax);
				result = false;
			}

			if(result)
			{
				Debug.Log("敵テーブルに問題はありませんでした");
			}
		}

		[System.Serializable]
		private class EnemyCreateTable : IProbability
		{
			[SerializeField]
			private CharacterMasterData masterData;

			[SerializeField]
			private int probability;

			public CharacterMasterData MasterData{ get { return this.masterData; } }

			public int Probability{ get { return this.probability; } }
		}

	}
}