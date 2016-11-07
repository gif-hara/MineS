using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EffectManager : SingletonMonoBehaviour<EffectManager>
	{
		public SerializeFieldGetter.GameObject prefabBattleEffectSlash;

		public SerializeFieldGetter.GameObject prefabDeadEffect;

		public SerializeFieldGetter.GameObject prefabStepEffect;

		public SerializeFieldGetter.GameObject prefabPoisonEffect;

		public SerializeFieldGetter.GameObject prefabLevelUp;

		public SerializeFieldGetter.GameObject prefabTakeDamage;

		public SerializeFieldGetter.GameObject prefabSummon;

		public SerializeFieldGetter.GameObject prefabAddAbnormalBuff;

		public SerializeFieldGetter.GameObject prefabAddAbnormalDebuff;
	}
}