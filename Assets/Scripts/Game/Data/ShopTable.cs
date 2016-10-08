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
	public class ShopTable
	{
		[System.Serializable]
		public class Element
		{
			[SerializeField]
			private int floorMin;

			[SerializeField]
			private int floorMax;

			[SerializeField][Range(0, 100)]
			private int probability;

			[SerializeField]
			private ItemTable itemTable;

			public bool IsMatchFloor(int floor)
			{
				return floor >= this.floorMin && floor <= this.floorMax;
			}

			public Inventory CreateInventory()
			{
				var result = new Inventory(null, GameDefine.ShopInventoryMax);
				for(int i = 0; i < GameDefine.ShopInventoryMax; i++)
				{
					result.AddItemNoLimit(this.itemTable.Create());
				}

				return result;
			}
		}

		[SerializeField]
		private List<Element> elements;

		public bool CanCreate(int floor)
		{
			return this.elements.Exists(e => e.IsMatchFloor(floor));
		}

		public Inventory CreateInventory(int floor)
		{
			var element = this.elements.Find(e => e.IsMatchFloor(floor));
			Debug.AssertFormat(element != null, "店データが無いのに道具袋を作ろうとしました. floor = {0}", floor);

			return element.CreateInventory();
		}
	}
}