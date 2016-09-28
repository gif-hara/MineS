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

		void OnDisable()
		{
		}

		public void OnModifiedInventory(Inventory inventory)
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.CreatePartition(this.equipmentPartitionName.Get);
			this.CreateEquipment(inventory, inventory.Equipment.Weapon, GameDefine.ItemType.Weapon);
			this.CreateEquipment(inventory, inventory.Equipment.Shield, GameDefine.ItemType.Shield);
			this.CreateEquipment(inventory, inventory.Equipment.Accessory, GameDefine.ItemType.Accessory);
			this.CreateEquipment(inventory, inventory.Equipment.Helmet, GameDefine.ItemType.Helmet);
			this.CreateEquipment(inventory, inventory.Equipment.Body, GameDefine.ItemType.Body);
			this.CreateEquipment(inventory, inventory.Equipment.Glove, GameDefine.ItemType.Glove);
			this.CreateEquipment(inventory, inventory.Equipment.Leg, GameDefine.ItemType.Leg);
			this.CreatePartition(this.inventoryPartitionName.Get);
			inventory.Items.ForEach(i => this.CreateCellController(this.GetImage(i, GameDefine.ItemType.UsableItem), this.GetMessage(i), this.GetUsableItemAction(inventory, i)));
		}

		private void CreateCellController(Sprite image, string message, CellClickActionBase action)
		{
			var cellController = Instantiate(this.cellPrefab, this.root, false) as CellController;
			this.createdObjects.Add(cellController.gameObject);
			var cellData = new CellData();
			cellData.SetController(cellController);
			cellController.SetCellData(cellData);
			cellData.BindCellClickAction(action);
			if(image != null)
			{
				cellController.SetImage(image);
			}
			if(!string.IsNullOrEmpty(message))
			{
				cellController.SetText(message);
			}
		}

		private void CreateEquipment(Inventory inventory, Item equipment, GameDefine.ItemType itemType)
		{
			var image = equipment == null ? TextureManager.Instance.defaultEquipment.Get(itemType) : equipment.InstanceData.Image;
			var message = equipment == null ? this.emptyMessage.Get : equipment.InstanceData.ItemName;
			var action = equipment == null ? null : new RemoveEquipmentAction(inventory, equipment);
			this.CreateCellController(image, message, action);
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
	}
}