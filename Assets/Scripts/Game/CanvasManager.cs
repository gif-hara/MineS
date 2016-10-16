﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CanvasManager : SingletonMonoBehaviour<CanvasManager>
	{
		[SerializeField]
		private Canvas effectLv0;

		[SerializeField]
		private Canvas effectLv1;

		public Canvas EffectLv0{ get { return this.effectLv0; } }

		public Canvas EffectLv1{ get { return this.effectLv1; } }
	}
}