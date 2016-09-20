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
	public class CharacterData
	{
		public string Name{ private set; get; }

		public int HitPointMax{ protected set; get; }

		public int HitPoint{ protected set; get; }

		public int MagicPointMax{ protected set; get; }

		public int MagicPoint{ protected set; get; }

		public int Strength{ protected set; get; }

		public int Armor{ protected set; get; }

		public int Experience{ protected set; get; }

		public int Money{ protected set; get; }

        public Sprite Image { protected set; get; }

		public void Initialize(string name, int hitPoint, int magicPoint, int strength, int armor, int experience, int money, Sprite image)
		{
			this.Name = name;
			this.HitPointMax = hitPoint;
			this.HitPoint = hitPoint;
			this.MagicPointMax = magicPoint;
			this.MagicPoint = magicPoint;
			this.Strength = strength;
			this.Armor = armor;
			this.Experience = experience;
            this.Money = money;
            this.Image = image;
		}

		public void Initialize(CharacterMasterData masterData)
		{
			this.Name = masterData.Name;
			this.HitPointMax = masterData.HitPoint;
			this.HitPoint = masterData.HitPoint;
			this.MagicPointMax = masterData.MagicPoint;
			this.MagicPoint = masterData.MagicPoint;
			this.Strength = masterData.Strength;
			this.Armor = masterData.Armor;
			this.Experience = masterData.Experience;
			this.Money = masterData.Money;
            this.Image = masterData.Image;
		}

		public void Recovery(int value)
		{
			this.HitPoint += value;
		}

		public void TakeDamage(int value, bool onlyHitPoint)
		{
			if(!onlyHitPoint)
			{
				this.Armor -= value;
				value = -this.Armor;
				value = value < 0 ? 0 : value;
				this.Armor = this.Armor < 0 ? 0 : this.Armor;
			}

			this.HitPoint -= value;
			this.HitPoint = this.HitPoint < 0 ? 0 : this.HitPoint;
		}

		public void AddMoney(int value)
		{
			this.Money += value;
			this.Money = this.Money > GameDefine.MoneyMax ? GameDefine.MoneyMax : this.Money;
		}

		public bool IsDead
		{
			get
			{
				return this.HitPoint <= 0;
			}
		}
	}
}