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
		public string Name{ get { return this.characterName.ToString(); } }

		[SerializeField]
		private StringAsset.Finder characterName;

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

		public List<GameDefine.AbilityType> AbilityTypes{ get { return this.abilityTypes; } }

		[SerializeField]
		private List<GameDefine.AbilityType> abilityTypes;

		public Sprite Image { get { return this.image; } }

		[SerializeField]
		private Sprite image;
	}
}