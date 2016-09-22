using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
	{
		[SerializeField]
		private List<CharacterDataObserver> characterDataObservers;

		[SerializeField]
		private List<InventoryObserver> inventoryObservers;

		[SerializeField]
		private GameObject inventoryUI;

		[SerializeField]
		private ExperienceData experienceData;

		[SerializeField]
		private CharacterMasterData growthData;

		public PlayerData Data{ private set; get; }

		public ExperienceData ExperienceData{ get { return this.experienceData; } }

		protected override void Awake()
		{
			base.Awake();
			this.Data = new PlayerData();
		}

		void Start()
		{
			this.NotifyCharacterDataObservers();
			TurnManager.Instance.AddEvent(this.OnTurnProgress);
		}

		public void RecoveryHitPoint(int value, bool isLimit)
		{
			this.Data.RecoveryHitPoint(value, isLimit);
			this.NotifyCharacterDataObservers();
		}

		public void TakeDamage(int value, bool onlyHitPoint)
		{
			this.Data.TakeDamage(value, onlyHitPoint);
			this.NotifyCharacterDataObservers();
		}

		public void TakeDamageRaw(int value, bool onlyHitPoint)
		{
			this.Data.TakeDamageRaw(value, onlyHitPoint);
			this.NotifyCharacterDataObservers();
		}

		public void AddExperience(int value)
		{
			value = Calculator.GetFinalExperience(value, this.Data.AbnormalStatuses);
			this.Data.AddExperience(value);
			this.NotifyCharacterDataObservers();
			while(this.Data.CanLevelUp)
			{
				this.Data.LevelUp(this.growthData);
				this.NotifyCharacterDataObservers();
			}
		}

		public void RemoveInventoryItem(Item item)
		{
			this.Data.Inventory.RemoveItem(item);
		}

		public void ChangeItem(Item before, Item after)
		{
			this.Data.Inventory.ChangeItem(before, after);
		}

		public void AddMoney(int value)
		{
			this.Data.AddMoney(value);
			this.NotifyCharacterDataObservers();
		}

		public void NotifyCharacterDataObservers()
		{
			for(int i = 0; i < this.characterDataObservers.Count; i++)
			{
				this.characterDataObservers[i].ModifiedData(this.Data);
			}
		}

		public void OpenInventoryUI()
		{
			this.inventoryUI.SetActive(true);
			this.UpdateInventoryUI();
		}

		public void UpdateInventoryUI()
		{
			this.inventoryObservers.ForEach(i => i.ModifiedData(this.Data.Inventory));
		}

		public void AddAbnormalStatus(AbnormalStatusBase abnormalStatus)
		{
			this.Data.AddAbnormalStatus(abnormalStatus);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryHitPoint()
		{
			this.Data.RecoveryHitPoint(999, false);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryHitPointDying()
		{
			this.Data.RecoveryHitPoint(-this.Data.HitPoint + 1, false);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryArmor()
		{
			this.Data.RecoveryArmor(999);
			this.NotifyCharacterDataObservers();
		}

		public void DebugZeroArmor()
		{
			this.Data.RecoveryArmor(-this.Data.Armor);
			this.NotifyCharacterDataObservers();
		}

		private void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.Data.OnTurnProgress(type, turnCount);
			this.NotifyCharacterDataObservers();
			this.Data.PrintAbnormalStatus();
		}
	}
}