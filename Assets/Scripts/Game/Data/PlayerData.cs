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
		[SerializeField]
		private int level;

		[SerializeField]
		private CharacterMasterData growthData = null;

		public int Level{ get { return this.level; } }

		public Inventory Inventory{ private set; get; }

		public PlayerData()
		{
		}

		public PlayerData(CharacterMasterData masterData, CharacterMasterData growthData, CellController cellController)
		{
			this.Initialize(masterData, cellController);
			this.level = 1;
			this.Inventory = new Inventory(this, GameDefine.InventoryItemMax);
			this.growthData = growthData;
		}

		public void AddExperience(int value)
		{
			this.experience += value;
			PlayerManager.Instance.Serialize();
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
			var finalExperience = Calculator.GetFinalExperience(target.Experience, this);
			InformationManager.OnDefeat(target, finalExperience);
			base.Defeat(target);
			this.AddExperience(finalExperience);
			AchievementManager.Instance.AddDefeatedEnemy(1);

			while(this.CanLevelUp)
			{
				this.LevelUp(this.growthData);
			}
		}

		public override void Dead(CharacterData attacker)
		{
			InformationManager.GameOver();
			PlayerManager.Instance.CloseInventoryUI();
			PlayerManager.Instance.NotifyCharacterDataObservers();
			var resultManager = ResultManager.Instance;
			var causeMessage = attacker == null
				? resultManager.causeOtherDead.Element.Format(this.Name)
				: resultManager.causeEnemyDead.Element.Format(attacker.Name);
			resultManager.Invoke(GameDefine.GameResultType.GameOver, causeMessage);
			this.Inventory.RemoveAll();
			this.money = 0;
			this.OnChangeDungeon();
			DungeonSerializer.InvalidSaveData();
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
			this.level++;
			InformationManager.OnLevelUpPlayer(this, this.Level);
			this.hitPointMax += growthData.HitPoint;
			if(this.HitPoint <= this.HitPointMax)
			{
				this.hitPoint = this.HitPointMax;
			}

			this.AddBaseStrength(growthData.Strength);
			this.baseArmor += growthData.Armor;
			SEManager.Instance.PlaySE(SEManager.Instance.levelUp);
			Object.Instantiate(EffectManager.Instance.prefabLevelUp.Element, CanvasManager.Instance.EffectLv0.transform, false);
			PlayerManager.Instance.Serialize();
		}

		public void LevelDown(CharacterMasterData growthData)
		{
			this.level--;
			InformationManager.OnLevelDownPlayer(this, this.Level);
			this.hitPointMax -= growthData.HitPoint;
			this.TakeDamageRaw(null, growthData.HitPoint, true);

			this.AddBaseStrength(-growthData.Strength);
			this.baseArmor -= growthData.Armor;
			this.baseArmor = this.Armor < 0 ? 0 : this.Armor;
			this.cellController.SetImage(this.Image);
		}

		public void OnChangeDungeon()
		{
			this.hitPointMax = this.masterData.HitPoint;
			this.hitPoint = this.HitPointMax;
			this.baseArmor = this.ArmorMax;
			this.experience = 0;
			this.abnormalStatuses.Clear();
			this.level = 1;
			this.baseStrength = this.masterData.Strength;
			PlayerManager.Instance.Serialize();
		}

		public void Giveup()
		{
			this.Inventory.RemoveAll();
			this.money = 0;
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

			this.experience = PlayerManager.Instance.ExperienceData.NeedNextLevel(this.Level - 1);
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

			this.experience = PlayerManager.Instance.ExperienceData.NeedNextLevel(this.Level - 1);
		}

		public override void ForceDead()
		{
			throw new System.NotImplementedException();
		}

		public override void ReturnTown()
		{
			if(DungeonManager.Instance.CurrentDataAsDungeon == null)
			{
				InformationManager.OnHadNoEffect();
				return;
			}

			ResultManager.Instance.Invoke(GameDefine.GameResultType.ReturnInItem, ResultManager.Instance.causeReturnInItem.Element.Get);
			DungeonManager.Instance.ClearDungeon(GameDefine.GameResultType.ReturnInItem);
			InformationManager.ReturnTown(this);
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

		public bool IsXray
		{
			get
			{
				return this.FindAbnormalStatus(GameDefine.AbnormalStatusType.Xray) || this.FindAbility(GameDefine.AbilityType.Clairvoyance);
			}
		}

		private List<AbilityBase> GetEquipmentAbilities(GameDefine.ItemType type)
		{
			var equipment = this.Inventory.Equipment.Get(type);
			if(equipment == null)
			{
				return new List<AbilityBase>();
			}

			return (equipment.InstanceData as EquipmentInstanceData).Abilities;
		}

		public void Serialize(string key)
		{
			HK.Framework.SaveData.SetClass<PlayerData>(key, this);
			this.SerializeAbnormalStatuses(key);
			this.Inventory.Serialize(GetInventorySerializeKeyName(key));
		}

		public static PlayerData Deserialize(string key, CellController cellController)
		{
			var result = HK.Framework.SaveData.GetClass<PlayerData>(key, null);
			result.cellController = cellController;
			DeserializeAbnormalStatuses(key, result);
			result.Inventory = new Inventory(result, GameDefine.InventoryItemMax);
			result.Inventory.Deserialize(GetInventorySerializeKeyName(key));
			result.abilities = new List<AbilityBase>();

			return result;
		}

		private static string GetInventorySerializeKeyName(string key)
		{
			return string.Format("{0}_Inventory", key);
		}
	}
}