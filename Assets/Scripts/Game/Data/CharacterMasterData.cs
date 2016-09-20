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

		public int MagicPoint{ get { return this.magicPoint; } }

		[SerializeField]
		private int magicPoint;

		public int Strength{ get { return this.strength; } }

		[SerializeField]
		private int strength;

		public int Armor{ get { return this.armor; } }

		[SerializeField]
		private int armor;

		public int Experience{ get { return this.experience; } }

		[SerializeField]
		private int experience;

		public int Money{ get { return this.money; } }

		[SerializeField]
		private int money;

		public Sprite Image { get { return this.image; } }

		[SerializeField]
		private Sprite image;
	}
}