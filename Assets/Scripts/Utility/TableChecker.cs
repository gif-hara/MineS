using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class TableChecker
	{
		public void Check<T>(List<T> elements, System.Type tableType, int floorMax) where T : TableElementBase
		{
			var result = true;
			int floor = 1;
			T element = elements.Find(e => e.IsMatch(floor));
			while(element != null || floor < floorMax)
			{
				var existCount = elements.FindAll(e => e.IsMatch(floor)).Count;
				result = result && existCount == 1;
				if(existCount == 0)
				{
					Debug.AssertFormat(false, "Error {0} : floor = {1}のテーブルがありません.", tableType.Name, floor);
				}
				else if(existCount > 1)
				{
					Debug.AssertFormat(false, "Erorr {0} : floor = {1}にテーブルが複数存在しています.", tableType.Name, floor);
				}
				floor++;
				element = elements.Find(e => e.IsMatch(floor));
			}
			floor--;

			if(floor != floorMax)
			{
				Debug.AssertFormat(false, "Error {0} : 階数の最大値と一致していません. floor = {1} max = {2}", tableType.Name, floor, floorMax);
				result = false;
			}

			if(result)
			{
				Debug.LogFormat("OK {0} : テーブルに問題はありませんでした", tableType.Name);
			}
		}

	}
}