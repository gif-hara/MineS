using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class EquipmentInstanceData : ItemInstanceDataBase
	{
		[SerializeField]
		private int basePower;

		[SerializeField]
		private int brandingLimit;

		/// <summary>
		/// 抽出可能か.
		/// </summary>
		[SerializeField]
		private bool canExtraction;

		[SerializeField]
		private GameDefine.ItemType itemType;

		[SerializeField, EnumLabel("Ability", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> abilities;

		[SerializeField]
		private int level;

		public int Level{ get { return this.level; } }

		public int BrandingLimit{ get { return this.brandingLimit; } }

		public int Power
		{
			get
			{
                var fixedPower = this.Level;

				// アーマー修正値のみ倍率をかける
				if(this.itemType == GameDefine.ItemType.Shield)
				{
                    fixedPower *= 2;
                }
                return this.basePower + fixedPower;
			}
		}

		public List<AbilityBase> Abilities{ private set; get; }

		public override string ItemName
		{
			get
			{
				if(this.Level <= 0)
				{
					return base.ItemName;
				}

				return ItemManager.Instance.equipmentRevisedLevelName.Element.Format(base.ItemName, this.Level);
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		public bool CanExtraction
		{
			get
			{
				return this.canExtraction;
			}
		}

		public EquipmentInstanceData(ItemMasterDataBase masterData)
		{
			base.InternalCreateFromMasterData(this, masterData);
			var equipmentData = masterData as EquipmentMasterData;
			this.basePower = equipmentData.BasePower;
			this.brandingLimit = equipmentData.BrandingLimit;
			this.canExtraction = equipmentData.CanExtraction;
			this.itemType = equipmentData.ItemType;
			this.abilities = new List<GameDefine.AbilityType>(equipmentData.abilities);
			this.level = 0;
			this.Abilities = AbilityFactory.Create(this.abilities, null);
		}

		public EquipmentInstanceData()
		{
			
		}

		public void InitializeAbilities()
		{
			this.Abilities = AbilityFactory.Create(this.abilities, null);
		}

		public void SetAbilitiesHolder(CharacterData holder)
		{
			this.Abilities.ForEach(a => a.SetHolder(holder));
		}

		public void Synthesis(Item target)
		{
			var targetEquipmentData = target.InstanceData as EquipmentInstanceData;
			this.AddAbility(targetEquipmentData.Abilities);
			this.level += targetEquipmentData.Level;
		}

		public bool CanRemoveAbility(int index)
		{
			return index >= (this.MasterData as EquipmentMasterData).abilities.Count;
		}

		public void AddAbility(AbilityBase ability)
		{
			if(this.Abilities.Count >= this.brandingLimit)
			{
				return;
			}

			this.Abilities.Add(ability);
			this.abilities = this.Abilities.Select(a => a.Type).ToList();
		}

		public void AddAbility(List<AbilityBase> abilities)
		{
			this.Abilities.AddRange(abilities);
			if(this.Abilities.Count > this.brandingLimit)
			{
				this.Abilities.RemoveRange(this.brandingLimit, this.Abilities.Count - this.brandingLimit);
			}
			this.abilities = this.Abilities.Select(a => a.Type).ToList();
		}

		public void RemoveAbility(int index)
		{
			this.Abilities.RemoveAt(index);
			this.abilities = this.Abilities.Select(a => a.Type).ToList();
		}

		public void LevelUp()
		{
			this.level++;
		}

		public bool CanLevelUp
		{
			get
			{
				return this.Level < GameDefine.EquipmentLevelMax;
			}
		}

		public bool CanSynthesis
		{
			get
			{
				return this.Abilities.Count < this.brandingLimit;
			}
		}

		public bool ExistBranding
		{
			get
			{
				return this.Abilities.Count > 0;
			}
		}

		public int NeedLevelUpMoney
		{
			get
			{
				return 1000 + 200 * this.Level;
			}
		}

	}
}