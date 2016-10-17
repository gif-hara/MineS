﻿using UnityEngine;
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
	}
}