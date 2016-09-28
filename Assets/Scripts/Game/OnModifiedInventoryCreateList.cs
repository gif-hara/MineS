using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedInventoryCreateList : MonoBehaviour, IReceiveModifiedInventory
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private CellController cellPrefab;

		[SerializeField]
		private PartitionController partitionPrefab;

		[SerializeField]
		private StringAsset.Finder emptyMessage;

		[SerializeField]
		private StringAsset.Finder equipmentPartitionName;

		[SerializeField]
		private StringAsset.Finder inventoryPartitionName;

		private List<CellController> cellControllers = null;

		private List<GameObject> createdObjects = new List<GameObject>();

		public void OnModifiedInventory(Inventory inventory)
		{
			this.createdObjects.ForEach(o => Destroy(o));

			if(!inventory.ExchangeItemController.CanExchange)
			{
				this.CreatePartition(this.equipmentPartitionName.Get);
				this.CreateCellController(inventory.Equipment.Weapon, GameDefine.ItemType.Weapon, this.GetEquipmentAction(inventory, inventory.Equipment.Weapon));
				this.CreateCellController(inventory.Equipment.Shield, GameDefine.ItemType.Shield, this.GetEquipmentAction(inventory, inventory.Equipment.Shield));
				this.CreateCellController(inventory.Equipment.Accessory, GameDefine.ItemType.Accessory, this.GetEquipmentAction(inventory, inventory.Equipment.Accessory));
				this.CreateCellController(inventory.Equipment.Helmet, GameDefine.ItemType.Helmet, this.GetEquipmentAction(inventory, inventory.Equipment.Helmet));
				this.CreateCellController(inventory.Equipment.Body, GameDefine.ItemType.Body, this.GetEquipmentAction(inventory, inventory.Equipment.Body));
				this.CreateCellController(inventory.Equipment.Glove, GameDefine.ItemType.Glove, this.GetEquipmentAction(inventory, inventory.Equipment.Glove));
				this.CreateCellController(inventory.Equipment.Leg, GameDefine.ItemType.Leg, this.GetEquipmentAction(inventory, inventory.Equipment.Leg));
				this.CreatePartition(this.inventoryPartitionName.Get);
			}
			inventory.Items.ForEach(i => this.CreateCellController(i, GameDefine.ItemType.UsableItem, this.GetUsableItemAction(inventory, i)));
		}

		private void CreateCellController(Item item, GameDefine.ItemType itemType, CellClickActionBase action)
		{
			var cellController = Instantiate(this.cellPrefab, this.root, false) as CellController;
			this.createdObjects.Add(cellController.gameObject);
			var cellData = new CellData();
			cellData.SetController(cellController);
			cellController.SetCellData(cellData);
			cellData.BindCellClickAction(action);
			cellController.SetImage(this.GetImage(item, itemType));
			cellController.SetText(this.GetMessage(item));
		}

		private void CreatePartition(string message)
		{
			var partition = Instantiate(this.partitionPrefab, this.root, false) as PartitionController;
			partition.SetText(message);
			this.createdObjects.Add(partition.gameObject);
		}

		private void InitializeCellControllers()
		{
			if(this.cellControllers != null)
			{
				return;
			}
			this.cellControllers = new List<CellController>();
			for(int i = 0; i < GameDefine.InventoryItemMax; i++)
			{
				this.cellControllers.Add(Instantiate(this.cellPrefab, this.root, false) as CellController);
			}
		}

		private Sprite GetImage(Item item, GameDefine.ItemType type)
		{
			return item == null ? TextureManager.Instance.defaultEquipment.Get(type) : item.InstanceData.Image;
		}

		private string GetMessage(Item item)
		{
			return item == null ? this.emptyMessage.Get : item.InstanceData.ItemName;
		}

		private CellClickActionBase GetUsableItemAction(Inventory inventory, Item item)
		{
			if(!inventory.ExchangeItemController.CanExchange)
			{
				return new InvokeItemAction(item);
			}
			else
			{
				return new ChangeItemAction(item);
			}
		}

		private CellClickActionBase GetEquipmentAction(Inventory inventory, Item item)
		{
			if(item == null)
			{
				return null;
			}

			return new RemoveEquipmentAction(inventory, item);
		}
	}
}