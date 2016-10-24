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

		public PlayerData(CharacterMasterData masterData, CharacterMasterData growthData, CellController cellController)
		{
			this.Initialize(masterData, cellController);
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
			AchievementManager.Instance.AddGiveDamage(damage);

			if(this.Inventory.IsFreeSpace && this.FindAbility(GameDefine.AbilityType.Theft) && Calculator.IsSuccessTheft(this))
			{
				this.Inventory.AddItem(DungeonManager.Instance.CurrentDataAsDungeon.CreateItem());
			}
		}

		public override void TakeDamageRaw(CharacterData attacker, int value, bool onlyHitPoint)
		{
			AchievementManager.Instance.AddTakeDamage(value);
			base.TakeDamageRaw(attacker, value, onlyHitPoint);
			Object.Instantiate(EffectManager.Instance.prefabTakeDamage.Element, CanvasManager.Instance.EffectLv0.transform, false);
		}

		public override void Defeat(IAttack target)
		{
			InformationManager.OnDefeat(target);
			base.Defeat(target);
			this.AddExperience(Calculator.GetFinalExperience(target.Experience, this));
			AchievementManager.Instance.AddDefeatedEnemy(1);

			while(this.CanLevelUp)
			{
				this.LevelUp(this.growthData);
			}
		}

		public override void Dead(CharacterData attacker)
		{
			DungeonManager.Instance.AddChangeDungeonEvent(this.GameOver);
			InformationManager.GameOver();
			var resultManager = ResultManager.Instance;
			var causeMessage = attacker == null
				? resultManager.causeOtherDead.Element.Format(this.Name)
				: resultManager.causeEnemyDead.Element.Format(attacker.Name);
			resultManager.Invoke(GameDefine.GameResultType.GameOver, causeMessage);
		}

		public override string ColorCode
		{
			get
			{
				return GameDefine.GoodColorCode;
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

			this.AddBaseStrength(growthData.Strength);
			this.Armor += growthData.Armor;
			SEManager.Instance.PlaySE(SEManager.Instance.levelUp);
			Object.Instantiate(EffectManager.Instance.prefabLevelUp.Element, CanvasManager.Instance.EffectLv0.transform, false);
		}

		public void LevelDown(CharacterMasterData growthData)
		{
			this.Level--;
			InformationManager.OnLevelDownPlayer(this, this.Level);
			this.HitPointMax -= growthData.HitPoint;
			this.TakeDamageRaw(null, growthData.HitPoint, true);

			this.AddBaseStrength(-growthData.Strength);
			this.Armor -= growthData.Armor;
			this.Armor = this.Armor < 0 ? 0 : this.Armor;
		}

		public void OnChangeDungeon()
		{
			this.HitPointMax = this.masterData.HitPoint;
			this.HitPoint = this.HitPointMax;
			this.Armor = this.ArmorMax;
			this.Experience = 0;
			this.AbnormalStatuses.Clear();
			this.Level = 1;
			this.baseStrength = this.masterData.Strength;
		}

		public override void ForceLevelUp(int value)
		{
			var experienceData = PlayerManager.Instance.ExperienceData;
			for(int i = 0; i < value; i++)
			{
				if(!experienceData.CanLevelUp(this.Level))
				{
					break;
				}
				this.LevelUp(this.growthData);
			}

			this.Experience = PlayerManager.Instance.ExperienceData.NeedNextLevel(this.Level - 1);
		}

		public override void ForceLevelDown(int value)
		{
			var experienceData = PlayerManager.Instance.ExperienceData;
			for(int i = 0; i < value; i++)
			{
				if(!experienceData.CanLevelDown(this.Level))
				{
					break;
				}
				this.LevelDown(this.growthData);
			}

			this.Experience = PlayerManager.Instance.ExperienceData.NeedNextLevel(this.Level - 1);
		}

		public override void ForceDead()
		{
			throw new System.NotImplementedException();
		}

		public bool CanLevelUp
		{
			get
			{
				var experienceData = PlayerManager.Instance.ExperienceData;
				if(!experienceData.CanLevelUp(this.Level))
				{
					return false;
				}
				return this.Experience >= experienceData.NeedNextLevel(this.Level);
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

		public override int Evasion
		{
			get
			{
				return base.Evasion + this.Inventory.Equipment.TotalLuck;
			}
		}

		public override int HitProbability
		{
			get
			{
				return base.HitProbability + this.Inventory.Equipment.TotalLuck;
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

		public override GameDefine.CharacterType CharacterType
		{
			get
			{
				return GameDefine.CharacterType.Player;
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

		public void GameOver()
		{
			this.Inventory.RemoveAll();
			this.Money = 0;
			DungeonManager.Instance.RemoveChangeDungeonEvent(this.GameOver);
			this.OnChangeDungeon();
			PlayerManager.Instance.NotifyCharacterDataObservers();
		}
	}
}