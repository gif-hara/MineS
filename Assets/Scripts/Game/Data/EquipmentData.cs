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

		[SerializeField, EnumLabel("Ability", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> abilities;

		public int Level{ private set; get; }

		public int Power
		{
			get
			{
				return this.basePower + this.Level;
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

		public override ItemDataBase Clone
		{
			get
			{
				var result = ScriptableObject.CreateInstance<EquipmentData>();
				this.InternalClone(result);
				result.basePower = this.basePower;
				result.brandingLimit = this.brandingLimit;
				result.canExtraction = this.canExtraction;
				result.itemType = this.itemType;
				result.abilities = new List<GameDefine.AbilityType>(this.abilities);
				result.Level = this.Level;
				result.Abilities = AbilityFactory.Create(this.abilities, null);

				return result;
			}
		}

#if UNITY_EDITOR
		public static EquipmentData CreateFromCsv(List<string> csv, GameDefine.ItemType itemType)
		{
			var result = CreateInstance<EquipmentData>();
			result.itemType = itemType;
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/" + itemType.ToString() + "/" + itemType.ToString() + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.basePower = int.Parse(csv[5]);
			result.brandingLimit = int.Parse(csv[6]);
			result.canExtraction = bool.Parse(csv[7]);
			result.abilities = AbilityParser.Parse(csv[8]);

			return result;
		}
#endif

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

		public bool CanRemoveAbility(int index)
		{
			return index >= (this.MasterData as EquipmentData).abilities.Count;
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
				return Mathf.FloorToInt((this.purchasePrice / 10) * (this.Level + 1));
			}
		}

	}
}