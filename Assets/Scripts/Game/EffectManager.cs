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
		public SerializeFieldGetter.GameObject prefabDamage;

		public SerializeFieldGetter.GameObject prefabBattleEffectSlash;
	}
}