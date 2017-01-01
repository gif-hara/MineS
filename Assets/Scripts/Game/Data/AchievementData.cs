using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
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

        [SerializeField]
        private float playTimer;
		public float PlayTimer
        {
            set { this.playTimer = value; }
            get { return this.playTimer; }
        }

		public string PlayTimeToString(string format)
		{
			return format
				.Replace("hh", this.PlayTimeHours.ToString("00"))
				.Replace("mm", this.PlayTimeMinutes.ToString("00"))
				.Replace("ss", this.PlayTimeSeconds.ToString("00"))
				.Replace("ms", this.PlayTimeMilliSeconds.ToString("000"));
		}

		public float PlayTimeHours
        { 
			get
			{
				return Mathf.Floor(this.playTimer / 60 / 60);
            }
		}

		public float PlayTimeMinutes
        { 
			get
			{
				return (Mathf.Floor(this.playTimer / 60)) % 60;
            }
		}

		public float PlayTimeSeconds
        { 
			get
			{
				return Mathf.Floor(this.playTimer) % 60;
            }
		}

		public float PlayTimeMilliSeconds
        { 
			get
			{
				return (this.playTimer % 1) * 1000;
            }
		}

        public AchievementData()
		{
			this.defeatedEnemy = 0;
			this.giveDamage = 0;
			this.takeDamage = 0;
		}
	}
}