using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AchievementData
	{
		[SerializeField]
		private int defeatedEnemy;
		public int DefeatedEnemy
		{
			set{ this.defeatedEnemy = value; }
			get{ return this.defeatedEnemy; }
		}

		[SerializeField]
		private int giveDamage;
		public int GiveDamage
		{
			set{ this.giveDamage = value; }
			get{ return this.giveDamage; }
		}

		[SerializeField]
		private int takeDamage;
		public int TakeDamage
		{
			set{ this.takeDamage = value; }
			get{ return this.takeDamage; }
		}

		public AchievementData()
		{
			this.defeatedEnemy = 0;
			this.giveDamage = 0;
			this.takeDamage = 0;
		}
	}
}