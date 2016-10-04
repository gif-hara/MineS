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
	public class OnModifiedItemCreateConfirmList : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private ConfirmSelectItemButtonController controllerPrefab;

		[SerializeField]
		private StringAsset.Finder useFormat;

		[SerializeField]
		private StringAsset.Finder equipmentFormat;

		[SerializeField]
		private StringAsset.Finder removeFormat;

		[SerializeField]
		private StringAsset.Finder throwFormat;

		[SerializeField]
		private StringAsset.Finder putFormat;

		[SerializeField]
		private StringAsset.Finder descriptionFormat;

		[SerializeField]
		private StringAsset.Finder cancelFormat;

		private List<GameObject> createdObjects = new List<GameObject>();

		void OnDisable()
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.createdObjects.Clear();
		}

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
			if(item.InstanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.CreateOnUsableItem(item);
			}
			else if(GameDefine.IsEquipment(item.InstanceData.ItemType))
			{
				if(PlayerManager.Instance.Data.Inventory.Equipment.IsInEquipment(item))
				{
					this.CreateOnEquipmentFromEquipment(item);
				}
				else
				{
					this.CreateOnEquipmentFromInventory(item);
				}
			}
			else
			{
				Debug.AssertFormat(false, "不正な値です. ItemType = {0}", item.InstanceData.ItemType);
			}
		}

#endregion

		private void CreateOnUsableItem(Item selectItem)
		{
			this.Create(selectItem, this.useFormat.Get, GameDefine.SelectItemDecideType.Use);
			this.Create(selectItem, this.throwFormat.Get, GameDefine.SelectItemDecideType.Throw);
			this.Create(selectItem, this.putFormat.Get, GameDefine.SelectItemDecideType.Put);
			this.Create(selectItem, this.descriptionFormat.Get, GameDefine.SelectItemDecideType.Description);
			this.Create(selectItem, this.cancelFormat.Get, GameDefine.SelectItemDecideType.Cancel);
		}

		private void CreateOnEquipmentFromInventory(Item selectItem)
		{
			this.Create(selectItem, this.equipmentFormat.Get, GameDefine.SelectItemDecideType.Equipment);
			this.Create(selectItem, this.descriptionFormat.Get, GameDefine.SelectItemDecideType.Description);
			this.Create(selectItem, this.cancelFormat.Get, GameDefine.SelectItemDecideType.Cancel);
		}

		private void CreateOnEquipmentFromEquipment(Item selectItem)
		{
			this.Create(selectItem, this.removeFormat.Get, GameDefine.SelectItemDecideType.Remove);
			this.Create(selectItem, this.descriptionFormat.Get, GameDefine.SelectItemDecideType.Description);
			this.Create(selectItem, this.cancelFormat.Get, GameDefine.SelectItemDecideType.Cancel);
		}

		private void Create(Item selectItem, string message, GameDefine.SelectItemDecideType type)
		{
			var controller = (Instantiate(this.controllerPrefab, this.root, false) as ConfirmSelectItemButtonController);
			controller.Initialize(selectItem, message, type);
			this.createdObjects.Add(controller.gameObject);
		}
	}
}