using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class CharacterMasterData : ScriptableObject
	{
		public string Name{ get { return this.characterName; } }

		[SerializeField]
		private string characterName;

		public int HitPoint{ get { return this.hitPoint; } }

		[SerializeField]
		private int hitPoint;

		public int Strength{ get { return this.strength; } }

		[SerializeField]
		private int strength;

		public int Armor{ get { return this.armor; } }

		[SerializeField]
		private int armor;

		public int HitProbability{ get { return this.hitProbability; } }

		[SerializeField]
		private int hitProbability;

		public int Evasion{ get { return this.evasion; } }

		[SerializeField]
		private int evasion;

		public int Experience{ get { return this.experience; } }

		[SerializeField]
		private int experience;

		public int Money{ get { return this.money; } }

		[SerializeField]
		private int money;

		public int DropItemProbability{ get { return this.dropItemProbability; } }

		[SerializeField]
		private int dropItemProbability;

		public List<ItemDataBase> OverrideDropItems{ get { return this.overrideDropItems; } }

		[SerializeField]
		private List<ItemDataBase> overrideDropItems;

		public List<GameDefine.AbilityType> AbilityTypes{ get { return this.abilityTypes; } }

		[EnumLabel("アビリティ", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> abilityTypes;

		public Sprite Image { get { return this.image; } }

		[SerializeField]
		private Sprite image;

		public static CharacterMasterData CreateFromCsv(List<string> csv)
		{
			var instance = ScriptableObject.CreateInstance<CharacterMasterData>();
			instance.characterName = csv[1];
			instance.hitPoint = int.Parse(csv[2]);
			instance.armor = int.Parse(csv[3]);
			instance.strength = int.Parse(csv[4]);
			instance.hitProbability = int.Parse(csv[5]);
			instance.evasion = int.Parse(csv[6]);
			instance.experience = int.Parse(csv[7]);
			instance.money = int.Parse(csv[8]);
			instance.dropItemProbability = int.Parse(csv[9]);
			// overrideDropItems.
			// ability.

			return instance;
		}
	}
}