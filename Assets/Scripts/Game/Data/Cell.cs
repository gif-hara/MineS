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
	public class Cell
	{
		[SerializeField]
		public int x;

		[SerializeField]
		public int y;

		public Cell()
		{
			
		}

		public Cell(int y, int x)
		{
			this.x = x;
			this.y = y;
		}

		public void Serialize(string key)
		{
			HK.Framework.SaveData.SetClass<Cell>(key, this);
		}

		public static Cell Deserialize(string key)
		{
			return HK.Framework.SaveData.GetClass<Cell>(key, null);
		}

		public bool IsMatch(Cell other)
		{
			return this.x == other.x && this.y == other.y;
		}
	}
}