using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.EventSystems;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedInventoryCreateList : MonoBehaviour, IReceiveModifiedInventory, IBeginDragHandler
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

		private List<CellController> cellControllers = new List<CellController>();

		private List<GameObject> createdObjects = new List<GameObject>();

#region IBeginDragHandler implementation

		public void OnBeginDrag(PointerEventData eventData)
		{
			this.CancelDeployDescription();
		}

#endregion

		public void OnModifiedInventory(Inventory inventory)
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.createdObjects.Clear();

			switch(inventory.OpenType)
			{
			case GameDefine.InventoryModeType.Use:
				this.OpenUseMode(inventory);
			break;
			case GameDefine.InventoryModeType.Exchange:
				this.OpenExchangeMode(inventory);
			break;
			case GameDefine.InventoryModeType.BlackSmith_Reinforcement:
				this.OpenBlackSmith_Reinforcement(inventory);
			break;
			default:
				Debug.AssertFormat(false, "未実装です. openType = {0}", inventory.OpenType);
			break;
			}
		}

		private void OpenUseMode(Inventory inventory)
		{
			this.CreateEquipmentCells(inventory, true);
			this.CreateInventoryItemCells(inventory, true);
		}

		private void OpenExchangeMode(Inventory inventory)
		{
			this.CreateInventoryItemCells(inventory, false);
		}

		private void OpenBlackSmith_Reinforcement(Inventory inventory)
		{
			var list = inventory.AllItem;
			list.Where(i => GameDefine.IsEquipment(i.InstanceData.ItemType));
			list.ForEach(i => this.CreateCellController(i, GameDefine.ItemType.Weapon, new SelectItemAction(i)));
		}

		private void CreateEquipmentCells(Inventory inventory, bool createPartition)
		{
			if(createPartition)
			{
				this.CreatePartition(this.equipmentPartitionName.Get);
			}
			this.CreateCellController(inventory.Equipment.Weapon, GameDefine.ItemType.Weapon, new SelectItemAction(inventory.Equipment.Weapon));
			this.CreateCellController(inventory.Equipment.Shield, GameDefine.ItemType.Shield, new SelectItemAction(inventory.Equipment.Shield));
			this.CreateCellController(inventory.Equipment.Accessory, GameDefine.ItemType.Accessory, new SelectItemAction(inventory.Equipment.Accessory));
		}

		private void CreateInventoryItemCells(Inventory inventory, bool createPartition)
		{
			if(createPartition)
			{
				this.CreatePartition(this.inventoryPartitionName.Get);
			}
			inventory.Items.ForEach(i => this.CreateCellController(i, GameDefine.ItemType.UsableItem, this.GetUsableItemAction(inventory, i)));
		}

		private void CancelDeployDescription()
		{
			var selectedGameObject = EventSystem.current.currentSelectedGameObject;
			if(selectedGameObject == null)
			{
				return;
			}

			var cellController = selectedGameObject.GetComponent(typeof(CellController)) as CellController;
			if(cellController == null)
			{
				return;
			}

			cellController.CancelDeployDescription();
		}

		private void CreateCellController(Item item, GameDefine.ItemType itemType, CellClickActionBase action)
		{
			var cellController = Instantiate(this.cellPrefab, this.root, false) as CellController;
			this.cellControllers.Add(cellController);
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
				return new SelectItemAction(item);
			}
			else
			{
				return new ChangeItemAction(item);
			}
		}
	}
}