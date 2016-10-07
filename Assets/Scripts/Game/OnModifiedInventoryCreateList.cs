using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Serialization;

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
		private CellController basicCellPrefab;

		[SerializeField]
		private CellController descriptionCellPrefab;

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
			case GameDefine.InventoryModeType.BlackSmith_BrandingSelectBaseEquipment:
				this.OpenBlackSmith_BrandingSelectBaseEquipment(inventory);
			break;
			case GameDefine.InventoryModeType.BlackSmith_BrandingSelectTargetEquipment:
				this.OpenBlackSmith_BrandingSelectTargetEquipment(inventory);
			break;
			case GameDefine.InventoryModeType.BlackSmith_BrandingSelectAbility:
				this.OpenBlackSmith_BrandingSelectAbility(inventory);
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
			list = list.Where(i => i.InstanceData.ItemType == GameDefine.ItemType.Weapon || i.InstanceData.ItemType == GameDefine.ItemType.Shield).ToList();
			list.ForEach(i => this.CreateItemCellController(i, GameDefine.ItemType.Weapon, this.GetAction(inventory, i)));
		}

		private void OpenBlackSmith_BrandingSelectBaseEquipment(Inventory inventory)
		{
			var list = inventory.AllItem;
			list = list.Where(i => i.InstanceData.ItemType == GameDefine.ItemType.Weapon || i.InstanceData.ItemType == GameDefine.ItemType.Shield).ToList();
			list.ForEach(i => this.CreateItemCellController(i, GameDefine.ItemType.Weapon, this.GetAction(inventory, i)));
		}

		private void OpenBlackSmith_BrandingSelectTargetEquipment(Inventory inventory)
		{
			var list = inventory.AllItem;
			list = list.Where(i => (i.InstanceData.ItemType == GameDefine.ItemType.Weapon || i.InstanceData.ItemType == GameDefine.ItemType.Shield) && i != inventory.SelectItem).ToList();
			list.ForEach(i => this.CreateItemCellController(i, GameDefine.ItemType.Weapon, this.GetAction(inventory, i)));
		}

		private void OpenBlackSmith_BrandingSelectAbility(Inventory inventory)
		{
			var brandingTargetEquipment = BlackSmithManager.Instance.BrandingTargetEquipment;
			(brandingTargetEquipment.InstanceData as EquipmentData).Abilities.ForEach(a => this.CreateAbilityCellController(a, null));
		}

		private void CreateEquipmentCells(Inventory inventory, bool createPartition)
		{
			if(createPartition)
			{
				this.CreatePartition(this.equipmentPartitionName.Get);
			}
			this.CreateItemCellController(inventory.Equipment.Weapon, GameDefine.ItemType.Weapon, this.GetAction(inventory, inventory.Equipment.Weapon));
			this.CreateItemCellController(inventory.Equipment.Shield, GameDefine.ItemType.Shield, this.GetAction(inventory, inventory.Equipment.Shield));
			this.CreateItemCellController(inventory.Equipment.Accessory, GameDefine.ItemType.Accessory, this.GetAction(inventory, inventory.Equipment.Accessory));
		}

		private void CreateInventoryItemCells(Inventory inventory, bool createPartition)
		{
			if(createPartition)
			{
				this.CreatePartition(this.inventoryPartitionName.Get);
			}
			inventory.Items.ForEach(i => this.CreateItemCellController(i, GameDefine.ItemType.UsableItem, this.GetAction(inventory, i)));
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

		private void CreateItemCellController(Item item, GameDefine.ItemType itemType, CellClickActionBase action)
		{
			var cellController = Instantiate(this.basicCellPrefab, this.root, false) as CellController;
			this.cellControllers.Add(cellController);
			this.createdObjects.Add(cellController.gameObject);
			var cellData = new CellData();
			cellData.SetController(cellController);
			cellController.SetCellData(cellData);
			cellData.BindCellClickAction(action);
			cellController.SetImage(this.GetImage(item, itemType));
			cellController.SetText(this.GetMessage(item));
		}

		private void CreateAbilityCellController(AbilityBase ability, CellClickActionBase action)
		{
			var cellController = Instantiate(this.descriptionCellPrefab, this.root, false) as CellController;
			this.cellControllers.Add(cellController);
			this.createdObjects.Add(cellController.gameObject);
			var cellData = new CellData();
			cellData.SetController(cellController);
			cellController.SetCellData(cellData);
			cellController.SetDescriptionData(ability.DescriptionKey);
			cellData.BindCellClickAction(action);
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

		private CellClickActionBase GetAction(Inventory inventory, Item item)
		{
			switch(inventory.OpenType)
			{
			case GameDefine.InventoryModeType.Use:
				return new SelectItemAction(item);
			case GameDefine.InventoryModeType.Exchange:
				return new ChangeItemAction(item);
			case GameDefine.InventoryModeType.BlackSmith_Reinforcement:
				return new SelectBlackSmithReinforcementItemAction(item);
			case GameDefine.InventoryModeType.BlackSmith_BrandingSelectBaseEquipment:
				return new SelectBlackSmithBlandingSelectBaseEquipmentAction(item);
			case GameDefine.InventoryModeType.BlackSmith_BrandingSelectTargetEquipment:
				return new SelectBlackSmithBlandingSelectTargetEquipmentAction(item);
			case GameDefine.InventoryModeType.BlackSmith_BrandingSelectAbility:
				Debug.AssertFormat(false, "アビリティを選択中はここでActionを返しません.");
				return null;
			default:
				Debug.AssertFormat(false, "未実装です. openType = {0}", inventory.OpenType);
				return null;
			}
		}
	}
}