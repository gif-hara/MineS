using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System;

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

		public virtual int Strength{ protected set; get; }

		public int Armor{ protected set; get; }

		public virtual int ArmorMax{ protected set; get; }

		public int Experience{ protected set; get; }

		public int Money{ protected set; get; }

		public List<Buff> Buffs{ protected set; get; }

		public List<Debuff> Debuffs{ protected set; get; }

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
			this.ArmorMax = GameDefine.ArmorMax;
			this.Experience = experience;
			this.Money = money;
			this.Buffs = new List<Buff>();
			this.Debuffs = new List<Debuff>();
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

		public void RecoveryHitPoint(int value, bool isLimit)
		{
			if(this.HitPoint >= this.HitPointMax && isLimit)
			{
				return;
			}

			this.HitPoint += value;

			if(isLimit)
			{
				this.HitPoint = this.HitPoint > this.HitPointMax ? this.HitPointMax : this.HitPoint;
			}
		}

		public void RecoveryArmor(int value)
		{
			this.Armor += value;
			this.Armor = this.Armor > this.ArmorMax ? this.ArmorMax : this.Armor;
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

		public void AddBuff(Buff newBuff)
		{
			this.AddAbnormalStatus<Buff, GameDefine.BuffType, GameDefine.DebuffType>(this.Buffs, newBuff);
			this.RemoveAbnormalStatus<Debuff, GameDefine.DebuffType, GameDefine.BuffType>(this.Debuffs, newBuff.OppositeType);
		}

		public void AddDebuff(Debuff newDebuff)
		{
			this.AddAbnormalStatus<Debuff, GameDefine.DebuffType, GameDefine.BuffType>(this.Debuffs, newDebuff);
			this.RemoveAbnormalStatus<Buff, GameDefine.BuffType, GameDefine.DebuffType>(this.Buffs, newDebuff.OppositeType);
		}

		public bool IsDead
		{
			get
			{
				return this.HitPoint <= 0;
			}
		}

		private void AddAbnormalStatus<A, T, O>(List<A> list, A newAbnormalStatus)
			where A : AbnormalStatusBase<T, O>
			where T : struct, IComparable
			where O : struct, IComparable
		{
			var oldIndex = list.FindIndex(a => a.Type.CompareTo(newAbnormalStatus.Type) == 0);
			if(oldIndex >= 0)
			{
				list[oldIndex] = newAbnormalStatus;
			}
			else
			{
				list.Add(newAbnormalStatus);
			}

			string log = "";
			list.ForEach(a => log += " " + a.Type.ToString());
			Debug.Log("Abnormal Status = " + log);
		}

		private void RemoveAbnormalStatus<A, T, O>(List<A> list, T target)
			where A : AbnormalStatusBase<T, O>
			where T : struct, IComparable
			where O : struct, IComparable
		{
			list.Remove(list.Find(a => a.Type.CompareTo(target) == 0));
		}
	}
}