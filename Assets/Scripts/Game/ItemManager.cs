using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ItemManager : SingletonMonoBehaviour<ItemManager>
	{
		[SerializeField]
		private UsableItemMasterData usableItemMasterData;

		[SerializeField]
		private WeaponMasterData weaponMasterData;

		[SerializeField]
		private ShieldMasterData shieldMasterData;

		[SerializeField]
		private HelmetMasterData helmetMasterData;

		[SerializeField]
		private BodyMasterData bodyMasterData;

		[SerializeField]
		private GloveMasterData gloveMasterData;

		[SerializeField]
		private LegMasterData legMasterData;

		[SerializeField]
		private AccessoryMasterData accessoryMasterData;

		[SerializeField]
		private List<InventoryObserver> observers;

		public UsableItemMasterData UsableItemMasterData{ get { return this.usableItemMasterData; } }

		public WeaponMasterData WeaponMasterData{ get { return this.weaponMasterData; } }

		public ShieldMasterData ShieldMasterData{ get { return this.shieldMasterData; } }

		public HelmetMasterData HelmetMasterData{ get { return this.helmetMasterData; } }

		public BodyMasterData BodyMasterData{ get { return this.bodyMasterData; } }

		public GloveMasterData GloveMasterData{ get { return this.gloveMasterData; } }

		public LegMasterData LegMasterData{ get { return this.legMasterData; } }

		public AccessoryMasterData AccessoryMasterData{ get { return this.accessoryMasterData; } }

		public void OpenInventory()
		{
			this.observers.ForEach(o => o.ModifiedData(PlayerManager.Instance.Data.Inventory));
		}
	}
}