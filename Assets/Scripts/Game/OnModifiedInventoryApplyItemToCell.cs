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
	public class OnModifiedInventoryApplyItemToCell : MonoBehaviour, IReceiveModifiedInventory
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private CellController cellPrefab;

		private List<CellController> cellControllers = null;

		public void OnModifiedInventory(Inventory data)
		{
			this.InitializeCellControllers();
			this.SetCellData(data);
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

		private void SetCellData(Inventory data)
		{
			for(int i = 0; i < this.cellControllers.Count; i++)
			{
				var item = data.Items[i];
				var cellController = this.cellControllers[i];
				var cellData = new CellData();
				this.cellControllers[i].SetCellData(cellData);
				cellData.BindCellClickAction(this.GetAction(item, cellController));
			}
		}

		private CellClickActionBase GetAction(Item item, CellController cellController)
		{
			if(!PlayerManager.Instance.Data.Inventory.ExchangeItemController.CanExchange)
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