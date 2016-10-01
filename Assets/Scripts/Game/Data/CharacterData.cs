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
	public abstract class CharacterData : IAttack
	{
		public string Name{ private set; get; }

		public int HitPointMax{ protected set; get; }

		public int HitPoint{ protected set; get; }

		public int FinalStrength
		{
			get
			{
				return Calculator.GetFinalStrength(this);
			}
		}

		public virtual int Strength
		{
			get
			{
				return this.baseStrength;
			}
		}

		public int BaseStrength
		{
			get
			{
				return this.baseStrength;
			}
		}

		protected int baseStrength = 0;

		public int Armor{ protected set; get; }

		public virtual int ArmorMax{ protected set; get; }

		public virtual int HitProbability{ get { return this.baseHitProbability + this.GetAbilityNumber(GameDefine.AbilityType.HitProbability); } }

		protected int baseHitProbability = 0;

		public virtual int Evasion{ get { return this.baseEvasion + this.GetAbilityNumber(GameDefine.AbilityType.Evasion); } }

		protected int baseEvasion = 0;

		public int Experience{ protected set; get; }

		public int Money{ protected set; get; }

		public List<AbnormalStatusBase> AbnormalStatuses{ protected set; get; }

		public virtual List<AbilityBase> Abilities{ get { return this.abilities; } }

		protected List<AbilityBase> abilities;

		public Sprite Image { protected set; get; }

		public void Initialize(string name, int hitPoint, int magicPoint, int strength, int armor, int hitProbability, int evasion, int experience, int money, Sprite image)
		{
			this.Name = name;
			this.HitPointMax = hitPoint;
			this.HitPoint = hitPoint;
			this.baseStrength = strength;
			this.Armor = armor;
			this.ArmorMax = armor;
			this.baseHitProbability = hitProbability;
			this.baseEvasion = evasion;
			this.Experience = experience;
			this.Money = money;
			this.AbnormalStatuses = new List<AbnormalStatusBase>();
			this.abilities = new List<AbilityBase>();
			this.Image = image;
		}

		public void Initialize(CharacterMasterData masterData)
		{
			this.Name = masterData.Name;
			this.HitPointMax = masterData.HitPoint;
			this.HitPoint = masterData.HitPoint;
			this.baseStrength = masterData.Strength;
			this.Armor = masterData.Armor;
			this.ArmorMax = masterData.Armor;
			this.baseHitProbability = masterData.HitProbability;
			this.baseEvasion = masterData.Evasion;
			this.Experience = masterData.Experience;
			this.Money = masterData.Money;
			this.AbnormalStatuses = new List<AbnormalStatusBase>();
			this.abilities = AbilityFactory.Create(masterData.AbilityTypes, this);
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

		public void Attack(CharacterData target)
		{
			if(this.CanAttack(target) != GameDefine.AttackResultType.Hit)
			{
				return;
			}

			var damage = target.TakeDamage(this, this.FinalStrength, FindAbility(GameDefine.AbilityType.Penetoration));
			if(this.FindAbility(GameDefine.AbilityType.Absorption))
			{
				this.RecoveryHitPoint(damage / 2, true);
			}
			if(this.FindAbility(GameDefine.AbilityType.Goemon))
			{
				this.Money += Calculator.GetGoemonValue(damage, this);
			}
			if(this.FindAbility(GameDefine.AbilityType.ContinuousAttack))
			{
				var ignoreEnemy = new List<CharacterData>();
				ignoreEnemy.Add(target);
				var otherTarget = EnemyManager.Instance.GetRandomEnemy(ignoreEnemy);
				if(otherTarget != null)
				{
					otherTarget.TakeDamageRaw(this, damage / 2, false);
				}
			}
		}

		public int TakeDamage(CharacterData attacker, int value, bool onlyHitPoint)
		{
			var resultDamage = Calculator.GetFinalDamage(value, this.AbnormalStatuses);
			this.TakeDamageRaw(attacker, resultDamage, onlyHitPoint);

			if(attacker != null && this.FindAbility(GameDefine.AbilityType.Splash))
			{
				attacker.TakeDamageRaw(this, resultDamage / 2, false);
			}

			return resultDamage;
		}

		public void TakeDamageRaw(CharacterData attacker, int value, bool onlyHitPoint)
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

			if(this.IsDead && attacker != null)
			{
				attacker.Defeat(this);
			}
		}

		public virtual void Defeat(CharacterData target)
		{
			target.Dead(this);
		}

		public abstract void Dead(CharacterData attacker);

		public void AddMoney(int value)
		{
			this.Money += value;
			this.Money = this.Money > GameDefine.MoneyMax ? GameDefine.MoneyMax : this.Money;
		}

		public void AddAbnormalStatus(AbnormalStatusBase newAbnormalStatus)
		{
			var oldIndex = this.AbnormalStatuses.FindIndex(a => a.Type == newAbnormalStatus.Type);
			if(oldIndex >= 0)
			{
				this.AbnormalStatuses[oldIndex] = newAbnormalStatus;
			}
			else
			{
				this.AbnormalStatuses.Add(newAbnormalStatus);
			}

			this.AbnormalStatuses.RemoveAll(a => a.Type == newAbnormalStatus.OppositeType);
		}

		public void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.AbnormalStatuses.ForEach(a => a.OnTurnProgress(type, turnCount));
			this.AbnormalStatuses.RemoveAll(a => !a.IsValid);
			this.Abilities.ForEach(a => a.OnTurnProgress());
		}

		public bool FindAbnormalStatus(GameDefine.AbnormalStatusType type)
		{
			return this.AbnormalStatuses.Find(a => a.Type == type) != null;
		}

		public bool FindAbility(GameDefine.AbilityType type)
		{
			return this.GetAbilityNumber(type) > 0;
		}

		public int GetAbilityNumber(GameDefine.AbilityType type)
		{
			// 封印状態なら常にfalseを返す.
			if(this.FindAbnormalStatus(GameDefine.AbnormalStatusType.Seal))
			{
				return 0;
			}

			return this.Abilities.FindAll(a => a.Type == type).Count;
		}

		public bool IsDead
		{
			get
			{
				return this.HitPoint <= 0;
			}
		}

		private GameDefine.AttackResultType CanAttack(CharacterData target)
		{
			if(this.FindAbnormalStatus(GameDefine.AbnormalStatusType.Fear))
			{
				return GameDefine.AttackResultType.MissByFear;
			}

			if(this.FindAbility(GameDefine.AbilityType.InbariablyHit))
			{
				return GameDefine.AttackResultType.Hit;
			}

			var success = this.HitProbability - target.Evasion;
			return (success > UnityEngine.Random.Range(0, 100)) ? GameDefine.AttackResultType.Hit : GameDefine.AttackResultType.Miss;
		}

		public void PrintAbnormalStatus()
		{
			string log = "";
			this.AbnormalStatuses.ForEach(a => log += " " + a.Type.ToString());
			Debug.Log("Abnormal Status = " + log);
		}
	}
}