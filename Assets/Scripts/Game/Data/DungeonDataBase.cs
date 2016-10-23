using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class DungeonDataBase : ScriptableObject
	{
		[SerializeField]
		private StringAsset.Finder dungeonName;

		[SerializeField]
		private bool itemIdentified;

		[SerializeField]
		private MapChipTable mapChipTable;

		[SerializeField]
		private ShopTable shopTable;

		public string Name{ get { return this.dungeonName.ToString(); } }

		public bool ItemIdentified{ get { return this.itemIdentified; } }

		public abstract CellData[,] Create(CellManager cellManager);

		public MapChipData GetMapChip(int floor)
		{
			return this.mapChipTable.Get(floor);
		}

		public Inventory CreateShopInventory(int floor)
		{
			return this.shopTable.CreateInventory(floor);
		}

		public bool CanCreateShop(int floor)
		{
			return this.shopTable.CanCreate(floor);
		}
	}
}