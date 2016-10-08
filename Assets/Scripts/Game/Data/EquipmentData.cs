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
	[System.Serializable][CreateAssetMenu()]
	public class EquipmentData : ItemDataBase
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

		[SerializeField]
		private List<GameDefine.AbilityType> abilities;

		[SerializeField]
		private EquipmentLevelElement reinforcementPurchase;

		[SerializeField]
		private EquipmentLevelElement levelPower;

		public int Level{ private set; get; }

		public int Power
		{
			get
			{
				if(this.Level <= 0)
				{
					return this.basePower;
				}

				return Mathf.FloorToInt(this.basePower + this.levelPower.Get(this.Level - 1));
			}
		}

		public List<AbilityBase> Abilities{ private set; get; }

		public override string ItemName
		{
			get
			{
				return EquipmentManager.Instance.GetStreetName(this) + base.ItemName;
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		public override ItemDataBase Clone
		{
			get
			{
				var result = ScriptableObject.CreateInstance<EquipmentData>();
				this.InternalClone(result);
				result.basePower = this.basePower;
				result.brandingLimit = this.brandingLimit;
				result.itemType = this.itemType;
				result.abilities = new List<GameDefine.AbilityType>(this.abilities);
				result.reinforcementPurchase = this.reinforcementPurchase;
				result.levelPower = this.levelPower;
				result.Level = this.Level;
				result.Abilities = AbilityFactory.Create(this.abilities, null);

				return result;
			}
		}

		public void SetAbilitiesHolder(CharacterData holder)
		{
			this.Abilities.ForEach(a => a.SetHolder(holder));
		}

		public void Synthesis(Item target)
		{
			var targetEquipmentData = target.InstanceData as EquipmentData;
			this.Abilities.AddRange(targetEquipmentData.Abilities);
			if(this.Abilities.Count > this.brandingLimit)
			{
				this.Abilities.RemoveRange(this.brandingLimit, this.Abilities.Count - this.brandingLimit);
			}
		}

		public void RemoveAbility(int index)
		{
			this.Abilities.RemoveAt(index);
		}

		public void LevelUp()
		{
			this.Level++;
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
				return Mathf.FloorToInt(this.purchasePrice * this.reinforcementPurchase.Get(this.Level));
			}
		}
	}
}