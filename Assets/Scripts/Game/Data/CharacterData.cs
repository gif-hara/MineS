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
	public abstract class CharacterData : IAttack, ITurnProgress
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

		public virtual int ArmorMax{ get { return this.baseArmorMax + Calculator.GetExquisiteArmorValue(this); } }

		protected int baseArmorMax;

		public virtual int HitProbability{ get { return this.baseHitProbability + this.GetAbilityNumber(GameDefine.AbilityType.HitProbability); } }

		protected int baseHitProbability = 0;

		public virtual int Evasion{ get { return this.baseEvasion + this.GetAbilityNumber(GameDefine.AbilityType.Evasion); } }

		protected int baseEvasion = 0;

		public int Experience{ protected set; get; }

		public int Money{ protected set; get; }

		public int DropItemProbability{ protected set; get; }

		public List<ItemDataBase> OverrideDropItems{ protected set; get; }

		public List<AbnormalStatusBase> AbnormalStatuses{ protected set; get; }

		public virtual List<AbilityBase> Abilities{ get { return this.abilities; } }

		protected List<AbilityBase> abilities;

		public Sprite Image { protected set; get; }

		public void Initialize(CharacterMasterData masterData)
		{
			this.Name = masterData.Name;
			this.HitPointMax = masterData.HitPoint;
			this.HitPoint = masterData.HitPoint;
			this.baseStrength = masterData.Strength;
			this.Armor = masterData.Armor;
			this.baseArmorMax = masterData.Armor;
			this.baseHitProbability = masterData.HitProbability;
			this.baseEvasion = masterData.Evasion;
			this.Experience = masterData.Experience;
			this.Money = masterData.Money;
			this.DropItemProbability = masterData.DropItemProbability;
			this.OverrideDropItems = new List<ItemDataBase>(masterData.OverrideDropItems);
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
			var hitResult = this.CanAttack(target);
			if(hitResult != GameDefine.AttackResultType.Hit)
			{
				if(hitResult == GameDefine.AttackResultType.Miss)
				{
					InformationManager.OnMiss(this.Name);
				}
				else if(hitResult == GameDefine.AttackResultType.MissByFear)
				{
					InformationManager.OnMissByFear(this.Name);
				}
				return;
			}

			this.GiveDamage(target, FindAbility(GameDefine.AbilityType.Penetoration));
		}

		protected virtual void OnAttacked(CharacterData target, int damage)
		{
			if(this.FindAbility(GameDefine.AbilityType.Absorption))
			{
				this.RecoveryHitPoint(damage / 2, true);
			}
			if(this.FindAbility(GameDefine.AbilityType.Recovery))
			{
				this.RecoveryHitPoint(Calculator.GetAbilityRecoveryValue(this), true);
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
					this.GiveDamageRaw(otherTarget, damage / 2, false);
					InformationManager.OnContinuousAttack(target.Name, (damage / 2));
				}
			}
			if(this.FindAbility(GameDefine.AbilityType.RiskOfLife))
			{
				this.TakeDamageArmorOnly(Calculator.GetRiskOfLifeSubArmorValue(this));
			}
			if(target.IsDead && this.FindAbility(GameDefine.AbilityType.Repair))
			{
				this.RecoveryArmor(Calculator.GetRepairValue(this));
			}

			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.PoisonPainted, GameDefine.AbnormalStatusType.Poison);
			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.Absentmindedness, GameDefine.AbnormalStatusType.Blur);
			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.VitalsPoke, GameDefine.AbnormalStatusType.Gout);
			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.BladeBroken, GameDefine.AbnormalStatusType.Dull);
			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.Derangement, GameDefine.AbnormalStatusType.Confusion);
			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.Intimidation, GameDefine.AbnormalStatusType.Fear);
			this.AddAbnormalStatusFromAbility(target, GameDefine.AbilityType.Curse, GameDefine.AbnormalStatusType.Seal);
		}

		protected void GiveDamage(CharacterData target, bool onlyHitPoint)
		{
			var damage = target.TakeDamage(this, this.FinalStrength, onlyHitPoint);
			this.OnAttacked(target, damage);
			if(target.IsDead)
			{
				this.Defeat(target);
			}
		}

		protected void GiveDamageRaw(CharacterData target, int damage, bool onlyHitPoint)
		{
			target.TakeDamageRaw(this, damage, onlyHitPoint);
			if(target.IsDead)
			{
				this.Defeat(target);
			}
		}

		public int TakeDamage(CharacterData attacker, int value, bool onlyHitPoint)
		{
			var resultDamage = Calculator.GetFinalDamage(value, this);
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

			if(this.IsDead)
			{
				this.Dead(attacker);
			}
		}

		public void TakeDamageArmorOnly(int value)
		{
			this.Armor -= value;
			this.Armor = this.Armor < 0 ? 0 : this.Armor;
		}

		public virtual void Defeat(CharacterData target)
		{
		}

		public abstract void Dead(CharacterData attacker);

		public void AddMoney(int value)
		{
			this.Money += value;
			this.Money = this.Money > GameDefine.MoneyMax ? GameDefine.MoneyMax : this.Money;
		}

		public void AddAbnormalStatus(AbnormalStatusBase newAbnormalStatus)
		{
			if(this.AbnormalStatuses.FindIndex(a => a.Type == newAbnormalStatus.Type) >= 0)
			{
				return;
			}
			else
			{
				this.AbnormalStatuses.Add(newAbnormalStatus);
			}

			if(GameDefine.IsBuff(newAbnormalStatus.Type))
			{
				newAbnormalStatus.AddRemainingTurn(Calculator.GetEnhancementAddTurn(this));
				newAbnormalStatus.AddRemainingTurn(Calculator.GetWeakSubTurn(this));
			}
			else if(!GameDefine.IsBuff(newAbnormalStatus.Type))
			{
				newAbnormalStatus.AddRemainingTurn(Calculator.GetInfectionAddTurn(this));
				newAbnormalStatus.AddRemainingTurn(Calculator.GetImmunitySubTurn(this));
			}

			this.AbnormalStatuses.RemoveAll(a => a.Type == newAbnormalStatus.OppositeType);
		}

		public void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.AbnormalStatuses.ForEach(a => a.OnTurnProgress(type, turnCount));
			this.AbnormalStatuses.RemoveAll(a => !a.IsValid);
			this.Abilities.ForEach(a => a.OnTurnProgress(type, turnCount));
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

		private void AddAbnormalStatusFromAbility(CharacterData target, GameDefine.AbilityType abilityType, GameDefine.AbnormalStatusType abnormalStatusType)
		{
			if(!Calculator.CanAddAbnormalStatusFromAbility(abilityType, this))
			{
				return;
			}

			target.AddAbnormalStatus(AbnormalStatusFactory.Create(abnormalStatusType, target, 5, 1));
		}
	}
}