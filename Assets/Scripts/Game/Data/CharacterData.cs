using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public abstract class CharacterData : IAttack, ITurnProgress
	{
		[SerializeField]
		protected string name;

		[SerializeField]
		protected int hitPointMax;

		[SerializeField]
		protected int hitPoint;

		[SerializeField]
		protected int baseStrength = 0;

		[SerializeField]
		protected int baseArmorMax;

		[SerializeField]
		protected int baseArmor;

		[SerializeField]
		protected int baseHitProbability = 0;

		[SerializeField]
		protected int baseEvasion = 0;

		[SerializeField]
		protected int experience;

		[SerializeField]
		protected int money;

		[SerializeField]
		protected int dropItemProbability;

		[SerializeField]
		protected List<ItemDataBase> overrideDropItems;

		[SerializeField]
		protected List<AbnormalStatusBase> abnormalStatuses;

		[SerializeField]
		protected List<AbilityBase> abilities;

		[SerializeField]
		protected List<GameDefine.AbilityType> abilityTypes;

		[SerializeField]
		private Sprite image;

		[SerializeField]
		protected CharacterMasterData masterData;

		public string Name{ get { return this.name; } }

		public int HitPointMax{ get { return this.hitPointMax; } }

		public int HitPoint{ get { return this.hitPoint; } }

		public int FinalStrength{ get { return Calculator.GetFinalStrength(this); } }

		public virtual int Strength{ get { return this.baseStrength; } }

		public int BaseStrength{ get { return this.baseStrength; } }

		public int Armor{ get { return this.baseArmor; } }

		public virtual int ArmorMax{ get { return this.baseArmorMax + Calculator.GetExquisiteArmorValue(this); } }

		public virtual int HitProbability{ get { return this.baseHitProbability + this.GetAbilityNumber(GameDefine.AbilityType.HitProbability); } }

		public virtual int Evasion{ get { return this.baseEvasion + this.GetAbilityNumber(GameDefine.AbilityType.Evasion); } }

		public int Experience{ get { return this.experience; } }

		public int Money{ get { return this.money; } }

		public int DropItemProbability{ get { return this.dropItemProbability; } }

		public List<ItemDataBase> OverrideDropItems{ get { return this.overrideDropItems; } }

		public List<AbnormalStatusBase> AbnormalStatuses{ get { return this.abnormalStatuses; } }

		public virtual List<AbilityBase> Abilities{ get { return this.abilities; } }

		public Sprite Image { get { return this.image; } }

		public abstract GameDefine.CharacterType CharacterType{ get; }

		protected CellController cellController;

		public void Initialize(CharacterMasterData masterData, CellController cellController)
		{
			this.masterData = masterData;
			this.name = masterData.Name;
			this.hitPointMax = masterData.HitPoint;
			this.hitPoint = masterData.HitPoint;
			this.baseStrength = masterData.Strength;
			this.baseArmor = masterData.Armor;
			this.baseArmorMax = masterData.Armor;
			this.baseHitProbability = masterData.HitProbability;
			this.baseEvasion = masterData.Evasion;
			this.experience = masterData.Experience;
			this.money = masterData.Money;
			this.dropItemProbability = masterData.DropItemProbability;
			this.overrideDropItems = new List<ItemDataBase>(masterData.OverrideDropItems);
			this.abnormalStatuses = new List<AbnormalStatusBase>();
			this.abilities = AbilityFactory.Create(masterData.AbilityTypes, this);
			this.abilityTypes = masterData.AbilityTypes;
			this.image = masterData.Image;
			this.cellController = cellController;
		}

		public void RecoveryHitPoint(int value, bool isLimit)
		{
			if(this.IsDead)
			{
				return;
			}

			SEManager.Instance.PlaySE(SEManager.Instance.recovery);
			this.cellController.Recovery(value);
			if(this.HitPoint >= this.HitPointMax && isLimit)
			{
				return;
			}

			this.hitPoint += value;

			if(isLimit)
			{
				this.hitPoint = this.HitPoint > this.HitPointMax ? this.HitPointMax : this.HitPoint;
			}
		}

		public void RecoveryArmor(int value, bool playSE)
		{
			if(playSE)
			{
				SEManager.Instance.PlaySE(SEManager.Instance.recovery);
			}
			this.cellController.Recovery(value);
			this.baseArmor += value;
			this.baseArmor = this.Armor > this.ArmorMax ? this.ArmorMax : this.Armor;
		}

		public void Attack(CharacterData target)
		{
			SEManager.Instance.PlaySE(SEManager.Instance.slash);
			var hitResult = this.CanAttack(target);
			if(hitResult != GameDefine.AttackResultType.Hit)
			{
				if(hitResult == GameDefine.AttackResultType.Miss)
				{
					InformationManager.OnMiss(this);
				}
				else if(hitResult == GameDefine.AttackResultType.MissByFear)
				{
					InformationManager.OnMissByFear(this);
				}
				return;
			}

			this.GiveDamage(target, FindAbility(GameDefine.AbilityType.Penetoration));
			UnityEngine.Object.Instantiate(EffectManager.Instance.prefabBattleEffectSlash.Element, target.cellController.transform, false);
		}

		protected virtual void OnAttacked(CharacterData target, int damage)
		{
			InformationManager.OnAttack(this, target, damage);

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
				this.money += Calculator.GetGoemonValue(this);
			}
			if(this.FindAbility(GameDefine.AbilityType.ContinuousAttack))
			{
				var ignoreEnemy = new List<CharacterData>();
				ignoreEnemy.Add(target);
				var otherTarget = EnemyManager.Instance.GetRandomEnemy(ignoreEnemy);
				if(otherTarget != null)
				{
					var continuousDamage = damage / 2;
					this.GiveDamageRaw(otherTarget, continuousDamage, false);
					InformationManager.OnContinuousAttack(this, target, continuousDamage);
				}
			}
			if(this.FindAbility(GameDefine.AbilityType.RiskOfLife) && this.Armor > 0)
			{
				this.TakeDamageArmorOnly(Calculator.GetRiskOfLifeSubArmorValue(this), true);
			}
			if(target.IsDead && this.FindAbility(GameDefine.AbilityType.Repair))
			{
				this.RecoveryArmor(Calculator.GetRepairValue(this), true);
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
			this.OnTakedDamage(attacker, resultDamage, onlyHitPoint);

			return resultDamage;
		}

		public virtual void TakeDamageRaw(CharacterData attacker, int value, bool onlyHitPoint)
		{
			if(this.IsDead)
			{
				return;
			}

			SEManager.Instance.PlaySE(SEManager.Instance.damage);

			this.cellController.TakeDamage(value);
			if(!onlyHitPoint)
			{
				this.baseArmor -= value;
				value = -this.Armor;
				value = value < 0 ? 0 : value;
				this.baseArmor = this.Armor < 0 ? 0 : this.Armor;
			}

			this.hitPoint -= value;
			this.hitPoint = this.HitPoint < 0 ? 0 : this.HitPoint;

			if(this.IsDead)
			{
				this.Dead(attacker);
			}
		}

		protected virtual void OnTakedDamage(CharacterData attacker, int value, bool onlyHitPoint)
		{
			if(attacker != null && !this.IsDead && this.FindAbility(GameDefine.AbilityType.Splash))
			{
				attacker.TakeDamageRaw(this, Calculator.GetSplashDamage(this, value), false);
			}
		}

		public void TakeDamageArmorOnly(int value, bool playSE)
		{
			if(playSE)
			{
				SEManager.Instance.PlaySE(SEManager.Instance.damage);
			}

			this.cellController.TakeDamage(value);
			this.baseArmor -= value;
			this.baseArmor = this.Armor < 0 ? 0 : this.Armor;
		}

		public virtual void Defeat(IAttack target)
		{
		}

		public abstract void Dead(CharacterData attacker);

		public abstract string ColorCode{ get; }

		public void AddMoney(int value)
		{
			this.money += value;
			this.money = this.Money > GameDefine.MoneyMax ? GameDefine.MoneyMax : this.Money;
		}

		public void AddBaseStrength(int value)
		{
			this.baseStrength += value;
			this.baseStrength = this.baseStrength < 1 ? 1 : this.baseStrength;
		}

		public void AddHitPointMax(int value)
		{
			this.hitPointMax += value;
			this.hitPointMax = this.HitPointMax < 1 ? 1 : this.HitPointMax;

			if(value > 0)
			{
				this.RecoveryHitPoint(value, true);
			}
			else
			{
				this.TakeDamageRaw(null, -value, true);
			}
		}

		public abstract void ForceLevelUp(int value);

		public abstract void ForceLevelDown(int value);

		public abstract void ForceDead();

		public GameDefine.AddAbnormalStatusResultType AddAbnormalStatus(AbnormalStatusBase newAbnormalStatus)
		{
			if(this.FindAbility(newAbnormalStatus.InvalidateAbilityType))
			{
				return GameDefine.AddAbnormalStatusResultType.Invalided;
			}

			if(this.AbnormalStatuses.FindIndex(a => a.Type == newAbnormalStatus.Type) >= 0)
			{
				return GameDefine.AddAbnormalStatusResultType.AlreadyHave;
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

			this.RemoveAbnormalStatus(newAbnormalStatus.OppositeType);

			return GameDefine.AddAbnormalStatusResultType.Added;
		}

		public void RemoveAbnormalStatus(GameDefine.AbnormalStatusType type)
		{
			this.abnormalStatuses.RemoveAll(a => a.Type == type);
		}

		public virtual void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.abnormalStatuses.ForEach(a => a.OnTurnProgress(type, turnCount));
			this.abnormalStatuses.RemoveAll(a => !a.IsValid);
			this.abilities.ForEach(a => a.OnTurnProgress(type, turnCount));
		}

		public virtual void OnLateTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			
		}

		public bool FindAbnormalStatus(GameDefine.AbnormalStatusType type)
		{
			return this.abnormalStatuses.Find(a => a.Type == type) != null;
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

			return this.abilities.Count(a => a.Type == type);
		}

		public void CheckArmorMax()
		{
			this.baseArmor = this.Armor > this.ArmorMax ? this.ArmorMax : this.Armor;
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

			var result = target.AddAbnormalStatus(AbnormalStatusFactory.Create(abnormalStatusType, target, 5, 1));
			if(result == GameDefine.AddAbnormalStatusResultType.Added)
			{
				InformationManager.OnAlsoAddAbnormalStatus(abnormalStatusType);
			}
		}

		protected void SerializeAbnormalStatuses(string key)
		{
			HK.Framework.SaveData.SetInt(GetAbnormalStatusCountKeyName(key), this.abnormalStatuses.Count);
			for(int i = 0; i < this.abnormalStatuses.Count; i++)
			{
				this.abnormalStatuses[i].Serialize(GetAbnormalStatusSerializeKeyName(key, i));
			}
		}

		protected static void DeserializeAbnormalStatuses(string key, CharacterData obj)
		{
			var count = HK.Framework.SaveData.GetInt(GetAbnormalStatusCountKeyName(key));
			obj.abnormalStatuses = new List<AbnormalStatusBase>();
			for(int i = 0; i < count; i++)
			{
				var abnormalStatus = AbnormalStatusBase.Deserialize(GetAbnormalStatusSerializeKeyName(key, i));
				abnormalStatus.SetHolder(obj);
				obj.abnormalStatuses.Add(abnormalStatus);
			}
		}

		private static string GetAbnormalStatusSerializeKeyName(string key, int index)
		{
			return string.Format("{0}_AbnormalStatus[{1}]", key, index);
		}

		private static string GetAbnormalStatusCountKeyName(string key)
		{
			return string.Format("{0}_AbnormalStatusCount", key);
		}
	}
}