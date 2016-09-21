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

		void Start()
		{
			this.Data = new PlayerData();
			this.NotifyCharacterDataObservers();
		}

		public void RecoveryHitPoint(int value, bool isLimit)
		{
			this.Data.RecoveryHitPoint(value, isLimit);
			this.NotifyCharacterDataObservers();
		}

		public void AddExperience(int value)
		{
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

		public void AddBuff(Buff buff)
		{
			this.Data.AddBuff(buff);
			this.NotifyCharacterDataObservers();
		}

		public void AddDebuff(Debuff debuff)
		{
			this.Data.AddDebuff(debuff);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryHitPoint()
		{
			this.Data.RecoveryHitPoint(999, false);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryArmor()
		{
			this.Data.RecoveryArmor(999);
			this.NotifyCharacterDataObservers();
		}
	}
}