using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public interface IAttack
	{
		string Name{ get; }

		int HitPointMax{ get; }

		int HitPoint{ get; }

		int Strength{ get; }

		int HitProbability{ get; }

		int Evasion{ get; }

		int Armor{ get; }

		int Experience{ get; }

		int Money{ get; }

		bool IsDead{ get; }

		GameDefine.CharacterType CharacterType{ get; }

		string ColorCode{ get; }

		CharacterMasterData MasterData{ get; }

        bool FindAbility(GameDefine.AbilityType type);

		bool FindAbnormalStatus(GameDefine.AbnormalStatusType type);

		int GetAbilityNumber(GameDefine.AbilityType type);

		void RecoveryHitPoint(int value, bool isLimit);

		void RecoveryArmor(int value, bool playSE);

		GameDefine.AddAbnormalStatusResultType AddAbnormalStatus(AbnormalStatusBase newAbnormalStatus);

		void AddAbility(AbilityBase newAbility);

		void RemoveAbnormalStatus(GameDefine.AbnormalStatusType type);

		void TakeDamageRaw(CharacterData attacker, int value, bool onlyHitPoint);

		void TakeDamageArmorOnly(int value, bool playSE);

		void AddBaseStrength(int value);

		void AddHitPointMax(int value);

		void ForceLevelUp(int value);

		void ForceLevelDown(int value);

		void ForceDead();

		void ReturnTown();

		void ChangeMasterData(CharacterMasterData masterData);
	}
}