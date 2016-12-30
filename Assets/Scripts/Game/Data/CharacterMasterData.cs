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

		[SerializeField]
		private StringAsset.Finder characterName;
		public virtual string Name{ get { return this.characterName.Get; } }

		[SerializeField]
		private int level;
		public int Level{ get{ return this.level; } }

		[SerializeField]
		private int hitPoint;
		public int HitPoint{ get { return this.hitPoint; } }

		[SerializeField]
		private int strength;
		public int Strength{ get { return this.strength; } }

		[SerializeField]
		private int armor;
		public int Armor{ get { return this.armor; } }

		[SerializeField]
		private int hitProbability;
		public int HitProbability{ get { return this.hitProbability; } }

		[SerializeField]
		private int evasion;
		public int Evasion{ get { return this.evasion; } }

		[SerializeField]
		private int experience;
		public int Experience{ get { return this.experience; } }

		[SerializeField]
		private int money;
		public int Money{ get { return this.money; } }

		[SerializeField]
		private int dropItemProbability;
		public int DropItemProbability{ get { return this.dropItemProbability; } }

		[SerializeField]
		private List<ItemMasterDataBase> overrideDropItems;
		public List<ItemMasterDataBase> OverrideDropItems{ get { return this.overrideDropItems; } }

		[EnumLabel("アビリティ", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> abilityTypes;
		public List<GameDefine.AbilityType> AbilityTypes{ get { return this.abilityTypes; } }

		[SerializeField]
		private Sprite image;
		public Sprite Image { get { return this.image; } }

		[SerializeField]
		private CharacterMasterData previousLevelData;
		public CharacterMasterData PreviousLevelData{ get { return this.previousLevelData; } }

		[SerializeField]
		private CharacterMasterData nextLevelData;
		public CharacterMasterData NextLevelData{ get { return this.nextLevelData; } }

#if UNITY_EDITOR
		public static CharacterMasterData CreateFromCsv(List<string> csv)
		{
			var instance = ScriptableObject.CreateInstance<CharacterMasterData>();
			var nameStringAsset = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/StringAsset/Enemy.asset", typeof(StringAsset)) as StringAsset;
			instance.characterName = nameStringAsset.CreateFinder(csv[1]);
			instance.level = int.Parse(csv[2]);
			instance.hitPoint = int.Parse(csv[3]);
			instance.armor = int.Parse(csv[4]);
			instance.strength = int.Parse(csv[5]);
			instance.hitProbability = int.Parse(csv[6]);
			instance.evasion = int.Parse(csv[7]);
			instance.experience = int.Parse(csv[8]);
			instance.dropItemProbability = int.Parse(csv[9]);
			instance.overrideDropItems = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemMasterDataBaseList>("Assets/DataSources/Item/ItemList/UsableItem.asset").Parse(csv[10]);
			instance.abilityTypes = AbilityParser.Parse(csv[11]);
			instance.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Enemy/Enemy" + int.Parse(csv[0]) + ".png", typeof(Sprite)) as Sprite;

			return instance;
		}

		[ContextMenu("SetLevelData")]
		public void SetLevelData()
		{
			var startIndex = this.name.IndexOf("Enemy") + 5;
			var id = int.Parse(this.name.Substring(startIndex));
			var previousId = (id % 3) - 1;
			if(previousId >= 0)
			{
				this.previousLevelData = UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterMasterData>("Assets/DataSources/Enemy/Enemy" + (id - 1) + ".asset");
			}
			var nextId = (id % 3) + 1;
			if(nextId < 3)
			{
				this.nextLevelData = UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterMasterData>("Assets/DataSources/Enemy/Enemy" + (id + 1) + ".asset");
			}
		}
#endif
	}
}