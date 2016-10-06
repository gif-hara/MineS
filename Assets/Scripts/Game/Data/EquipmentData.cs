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

		private int level = 0;

		public int Power
		{
			get
			{
				if(this.level <= 0)
				{
					return this.basePower;
				}

				return Mathf.FloorToInt(this.basePower + this.levelPower.Get(this.level - 1));
			}
		}

		public List<AbilityBase> Abilities{ private set; get; }

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
				result.itemType = this.itemType;
				result.abilities = new List<GameDefine.AbilityType>(this.abilities);
				result.reinforcementPurchase = this.reinforcementPurchase;
				result.levelPower = this.levelPower;
				result.level = this.level;
				result.Abilities = AbilityFactory.Create(this.abilities, null);

				return result;
			}
		}

		public void SetAbilitiesHolder(CharacterData holder)
		{
			this.Abilities.ForEach(a => a.SetHolder(holder));
		}

		public void LevelUp()
		{
			this.level++;
		}

		public bool CanLevelUp
		{
			get
			{
				return this.level < GameDefine.EquipmentLevelMax;
			}
		}

		public int NeedLevelUpMoney
		{
			get
			{
				return Mathf.FloorToInt(this.purchasePrice * this.reinforcementPurchase.Get(this.level));
			}
		}
	}
}