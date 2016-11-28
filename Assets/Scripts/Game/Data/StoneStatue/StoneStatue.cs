using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class StoneStatue
	{
		protected GameDefine.StoneStatueType type;

		public StoneStatue(GameDefine.StoneStatueType type)
		{
			this.type = type;
		}

		public GameDefine.StoneStatueType Type
		{
			get
			{
				return this.type;
			}
		}

		public virtual void OnTurnProgress()
		{
		}
	}
}