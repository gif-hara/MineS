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
	public sealed class CellData
	{
		public Cell Position{ private set; get; }

		private CellSerializeData serializeData = new CellSerializeData();

		public int MapChipId{ get { return this.serializeData.mapChipId; } }

		public bool CanStep{ get { return this.serializeData.canStep; } }

		public bool IsIdentification{ get { return this.serializeData.isIdentification; } }

		public CellController Controller{ private set; get; }

		private CellClickActionBase cellClickAction;

		private DeployDescriptionBase deployDescription;

		private System.Action<GameDefine.ActionableType> infeasibleEvent = null;

		private System.Action<bool> modifiedCanStepEvent = null;

		private System.Action<bool> modifiedIdentificationEvent = null;

		private System.Action<int> modifiedLockCountEvent = null;

		public CellData(CellController cellController)
		{
			this.Position = new Cell(-1, -1);
			this.serializeData.isIdentification = true;
			this.serializeData.lockCount = 0;
			this.serializeData.canStep = true;
			this.Controller = cellController;
		}

		public CellData(int y, int x, int mapChipId, CellController cellController)
		{
			this.Position = new Cell(y, x);
			this.serializeData.mapChipId = mapChipId;
			this.serializeData.isIdentification = false;
			this.serializeData.lockCount = 0;
			this.Controller = cellController;
		}

		public void Setup()
		{
			if(this.cellClickAction != null)
			{
				this.cellClickAction.SetCellController(this.Controller);
				this.cellClickAction.SetCellData(this);
			}
		}

		public void Action()
		{
			var actionableType = this.GetActionableType;
			if(actionableType != GameDefine.ActionableType.OK && this.infeasibleEvent != null)
			{
				this.infeasibleEvent(actionableType);
				return;
			}

			var isIdentification = this.Identification(true, PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray), true);
			if(this.cellClickAction != null)
			{
				var currentCellClickAction = this.cellClickAction;
				currentCellClickAction.Invoke(this);
				CellManager.Instance.OccurredEvent();
				if(isIdentification)
				{
					currentCellClickAction.OnIdentification(this);
				}
			}

			if(isIdentification)
			{
				TurnManager.Instance.Progress(GameDefine.TurnProgressType.CellClick);
				Object.Instantiate(EffectManager.Instance.prefabStepEffect.Element, this.Controller.transform, false);
			}
		}

		public void InvokeFromLightStoneStatue()
		{
			var isIdentification = this.Identification(true, PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray), false);
			if(this.cellClickAction != null)
			{
				var currentCellClickAction = this.cellClickAction;
				currentCellClickAction.InvokeFromLightStoneStatue(this);
				if(isIdentification)
				{
					currentCellClickAction.OnIdentification(this);
				}
			}
		}

		public void DebugAction()
		{
			var isIdentification = this.Identification(true, PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray), true);
			if(this.cellClickAction != null)
			{
				var currentCellClickAction = this.cellClickAction;
				currentCellClickAction.Invoke(this);
				if(isIdentification)
				{
					currentCellClickAction.OnIdentification(this);
				}
			}
		}

		public void Description()
		{
			if(this.deployDescription == null)
			{
				return;
			}

			this.deployDescription.Deploy();
		}

		public void OnUseXray()
		{
			if(this.cellClickAction == null)
			{
				return;
			}

			this.serializeData.isXray = true;
			this.cellClickAction.OnUseXray();
		}

		public void BindEvent(
			System.Action<GameDefine.ActionableType> infeasibleEvent,
			System.Action<bool> modifiedCanStepEvent,
			System.Action<bool> modifiedIdentificationEvent, 
			System.Action<int> modifiedLockCountEvent
		)
		{
			this.infeasibleEvent = infeasibleEvent;
			this.modifiedCanStepEvent = modifiedCanStepEvent;
			this.modifiedIdentificationEvent = modifiedIdentificationEvent;
			this.modifiedLockCountEvent = modifiedLockCountEvent;

			this.modifiedCanStepEvent(this.serializeData.canStep);
			this.modifiedIdentificationEvent(this.serializeData.isIdentification);
			this.modifiedLockCountEvent(this.serializeData.lockCount);
		}

		public void BindCellClickAction(CellClickActionBase cellClickAction)
		{
			this.cellClickAction = cellClickAction;
			if(this.cellClickAction != null)
			{
				this.Setup();
			}
		}

		public void BindDeployDescription(DeployDescriptionBase deployDescription)
		{
			this.deployDescription = deployDescription;
		}

		public void Steppable(bool isXray)
		{
			if(this.serializeData.canStep)
			{
				return;
			}

			if(isXray)
			{
				this.OnUseXray();
			}

			this.serializeData.canStep = true;
			if(this.modifiedCanStepEvent != null)
			{
				this.modifiedCanStepEvent(this.serializeData.canStep);
			}
		}

		public void AddLock()
		{
			this.serializeData.lockCount++;
			if(this.modifiedLockCountEvent != null)
			{
				this.modifiedLockCountEvent(this.serializeData.lockCount);
			}
		}

		public void ReleaseLock()
		{
			this.serializeData.lockCount--;
			this.serializeData.lockCount = this.serializeData.lockCount < 0 ? 0 : this.serializeData.lockCount;
			if(this.modifiedLockCountEvent != null)
			{
				this.modifiedLockCountEvent(this.serializeData.lockCount);
			}
		}

		public void ForceReleaseLock()
		{
			this.serializeData.lockCount = 0;
			if(this.modifiedLockCountEvent != null)
			{
				this.modifiedLockCountEvent(this.serializeData.lockCount);
			}
		}

		public bool Identification(bool isSteppableAdjacentCells, bool isXray, bool playSE)
		{
			if(this.serializeData.isIdentification)
			{
				return false;
			}

			if(isSteppableAdjacentCells)
			{
				var adjacentCells = CellManager.Instance.GetAdjacentCellDataLeftTopRightBottom(this.Position.y, this.Position.x);
				for(int i = 0; i < adjacentCells.Count; i++)
				{
					adjacentCells[i].Steppable(isXray);
				}
			}

			this.serializeData.isIdentification = true;
			if(this.modifiedIdentificationEvent != null)
			{
				this.modifiedIdentificationEvent(this.serializeData.isIdentification);
			}

			if(playSE)
			{
				var seManager = SEManager.Instance;
				seManager.PlaySE(seManager.walks[Random.Range(0, seManager.walks.Count)]);
			}

			return true;
		}

		public void OnNextFloor()
		{
			if(this.cellClickAction != null)
			{
				this.cellClickAction.OnNextFloor();
			}
		}

		public List<CellData> AdjacentCellAll
		{
			get
			{
				return CellManager.Instance.GetAdjacentCellDataAll(this.Position);
			}
		}

		public bool IsLock
		{
			get
			{
				return this.serializeData.lockCount > 0;
			}
		}

		public GameDefine.EventType CurrentEventType
		{
			get
			{
				if(this.cellClickAction == null)
				{
					return GameDefine.EventType.None;
				}

				return this.cellClickAction.EventType;
			}
		}

		public GameDefine.ActionableType GetActionableType
		{
			get
			{
				if(!this.serializeData.canStep)
				{
					return GameDefine.ActionableType.NotStep;
				}
				if(this.IsLock)
				{
					return GameDefine.ActionableType.Lock;
				}

				return GameDefine.ActionableType.OK;
			}
		}

		public void Serialize()
		{
			HK.Framework.SaveData.SetClass<CellSerializeData>(this.SerializeKeyName, this.serializeData);
			if(this.cellClickAction != null)
			{
				HK.Framework.SaveData.SetString(this.CellClickKeyName, this.cellClickAction.GetType().FullName);
				this.cellClickAction.Serialize(this.Position.y, this.Position.x);
			}
			else
			{
				HK.Framework.SaveData.Remove(this.CellClickKeyName);
			}
		}

		public void Deserialize()
		{
			this.serializeData = HK.Framework.SaveData.GetClass<CellSerializeData>(this.SerializeKeyName, null);
			if(HK.Framework.SaveData.ContainsKey(this.CellClickKeyName))
			{
				var cellClickType = System.Type.GetType(HK.Framework.SaveData.GetString(this.CellClickKeyName));
				this.cellClickAction = (CellClickActionBase)System.Activator.CreateInstance(cellClickType);
				this.cellClickAction.Deserialize(this.Position.y, this.Position.x);
			}
		}

		public void LateDeserialize()
		{
			if(this.cellClickAction == null)
			{
				return;
			}

			this.cellClickAction.LateDeserialize(this.Position.y, this.Position.x);
		}

		public void UseXrayOnDeserialize()
		{
			if(this.serializeData.isXray)
			{
				this.OnUseXray();
			}
		}

		private string SerializeKeyName
		{
			get
			{
				return string.Format("DungeonCellData_{0}_{1}", this.Position.y, this.Position.x);
			}
		}

		private string CellClickKeyName
		{
			get
			{
				return string.Format("CellClickAction_{0}_{1}", this.Position.y, this.Position.x);
			}
		}
	}
}