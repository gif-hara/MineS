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

		protected string descriptionKey;

		public StoneStatue(GameDefine.StoneStatueType type, string descriptionKey)
		{
			this.type = type;
			this.descriptionKey = descriptionKey;
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