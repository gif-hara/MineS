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
	public class PlayerData : CharacterData
	{
		public int Level{ private set; get; }

		public Inventory Inventory{ private set; get; }

		public CharacterMasterData growthData = null;

		public PlayerData(CharacterMasterData masterData, CharacterMasterData growthData)
		{
			this.Initialize(masterData);
			this.Level = 1;
			this.Inventory = new Inventory(this, GameDefine.InventoryItemMax);
			this.growthData = growthData;
		}

		public void AddExperience(int value)
		{
			this.Experience += value;
		}

		protected override void OnAttacked(CharacterData target, int damage)
		{
			base.OnAttacked(target, damage);

			if(this.Inventory.IsFreeSpace && this.FindAbility(GameDefine.AbilityType.Theft) && Calculator.IsSuccessTheft(this))
			{
				this.Inventory.AddItem(DungeonManager.Instance.CurrentData.CreateItem());
			}
		}

		public override void Defeat(CharacterData target)
		{
			InformationManager.OnDefeat(target);
			base.Defeat(target);
			this.AddExperience(Calculator.GetFinalExperience(target.Experience, this));
			this.AddMoney(Calculator.GetFinalMoney(target.Money, this));

			while(this.CanLevelUp)
			{
				this.LevelUp(this.growthData);
			}
		}

		public override void Dead(CharacterData attacker)
		{
		}

		public override string ColorCode
		{
			get
			{
				return "#00FFE9";
			}
		}

		public void OnInitiative(CharacterData enemy)
		{
			if(!this.FindAbility(GameDefine.AbilityType.Initiative))
			{
				return;
			}
			var damage = Calculator.GetInitiativeDamage(this);
			InformationManager.OnInitiativeDamage(this, enemy, damage);
			this.GiveDamageRaw(enemy, damage, false);
		}

		public void LevelUp(CharacterMasterData growthData)
		{
			this.Level++;
			InformationManager.OnLevelUpPlayer(this, this.Level);
			this.HitPointMax += growthData.HitPoint;
			if(this.HitPoint <= this.HitPointMax)
			{
				this.HitPoint = this.HitPointMax;
			}

			this.baseStrength += growthData.Strength;
			this.Armor += growthData.Armor;
		}

		public bool CanLevelUp
		{
			get
			{
				return this.Experience >= PlayerManager.Instance.ExperienceData.NeedNextLevel(this.Level);
			}
		}

		public override int Strength
		{
			get
			{
				return this.baseStrength + this.Inventory.Equipment.TotalStrength;
			}
		}

		public override int ArmorMax
		{
			get
			{
				return base.ArmorMax + this.Inventory.Equipment.TotalArmor;
			}
		}

		public override List<AbilityBase> Abilities
		{
			get
			{
				var result = new List<AbilityBase>();
				result.AddRange(this.abilities);
				result.AddRange(this.GetEquipmentAbilities(GameDefine.ItemType.Weapon));
				result.AddRange(this.GetEquipmentAbilities(GameDefine.ItemType.Shield));
				result.AddRange(this.GetEquipmentAbilities(GameDefine.ItemType.Accessory));

				return result;
			}
		}

		private List<AbilityBase> GetEquipmentAbilities(GameDefine.ItemType type)
		{
			var equipment = this.Inventory.Equipment.Get(type);
			if(equipment == null)
			{
				return new List<AbilityBase>();
			}

			return (equipment.InstanceData as EquipmentData).Abilities;
		}
	}
}