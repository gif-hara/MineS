using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ResultManager : SingletonMonoBehaviour<ResultManager>
	{
		[System.Serializable]
		public class AchievementElement
		{
			[SerializeField]
			private StringAsset.Finder title;

			[SerializeField]
			private StringAsset.Finder value;

			public string Title{ get { return this.title.Get; } }

			public string Value(int value)
			{
				return this.value.Format(value);
			}

            public string ValueFromDateTime(float totalHours, float minutes, float seconds, float milliSeconds)
            {
                return this.value.Get
				.Replace("hh", totalHours.ToString("00"))
                .Replace("mm", minutes.ToString("00"))
                .Replace("ss", seconds.ToString("00"))
                .Replace("ms", milliSeconds.ToString("000"));
            }
        }

        [SerializeField]
		private Image uiRoot;

		[SerializeField]
		private CanvasGroup contentRoot;

		[SerializeField]
		private Text causeText;

		[SerializeField]
		private Transform achievementParent;

		[SerializeField]
		private Color clearColor;

		[SerializeField]
		private Color gameOverColor;

		[SerializeField]
		private ResultAchievementElementController prefabElement;

		[SerializeField]
		private AchievementElement playTime;

		[SerializeField]
		private AchievementElement floor;

		[SerializeField]
		private AchievementElement level;

		[SerializeField]
		private AchievementElement hitPoint;

		[SerializeField]
		private AchievementElement armor;

		[SerializeField]
		private AchievementElement strength;

		[SerializeField]
		private AchievementElement money;

		[SerializeField]
		private AchievementElement defeatedEnemy;

		[SerializeField]
		private AchievementElement giveDamage;

		[SerializeField]
		private AchievementElement takeDamage;

		public SerializeFieldGetter.StringAssetFinder causeClear;

		public SerializeFieldGetter.StringAssetFinder causeReturnInItem;

		public SerializeFieldGetter.StringAssetFinder causeEnemyDead;

		public SerializeFieldGetter.StringAssetFinder causeOtherDead;

		private List<ResultAchievementElementController> createdObjects = new List<ResultAchievementElementController>();

		public bool IsResult{ private set; get; }

		void Start()
		{
			this.IsResult = false;
			DungeonManager.Instance.AddNextFloorEvent(this.OnNextFloor);
			DungeonManager.Instance.AddPreChangeDungeonEvent(this.OnPreChangeDungeon);
		}

		public void Invoke(GameDefine.GameResultType type, string causeMessage)
		{
			this.IsResult = true;
			this.causeText.text = causeMessage;
			var playerData = PlayerManager.Instance.Data;
			var achievementManager = AchievementManager.Instance;
			this.CreateAchievementElementFromDateTime(this.playTime, achievementManager.Data);
			this.CreateAchievementElement(this.floor, DungeonManager.Instance.Floor);
			this.CreateAchievementElement(this.level, playerData.Level);
			this.CreateAchievementElement(this.hitPoint, playerData.HitPointMax);
			this.CreateAchievementElement(this.armor, playerData.ArmorMax);
			this.CreateAchievementElement(this.strength, playerData.Strength);
			this.CreateAchievementElement(this.money, playerData.Money);
			this.CreateAchievementElement(this.defeatedEnemy, achievementManager.Data.DefeatedEnemy);
			this.CreateAchievementElement(this.giveDamage, achievementManager.Data.GiveDamage);
			this.CreateAchievementElement(this.takeDamage, achievementManager.Data.TakeDamage);

			this.uiRoot.gameObject.SetActive(true);
			this.uiRoot.color = GameDefine.IsPossitiveGameClear(type) ? this.clearColor : this.gameOverColor;
			this.contentRoot.alpha = 0.0f;
			DOTween.ToAlpha(() => new Color(), x => this.contentRoot.alpha = x.a, 1.0f, 1.0f)
				.SetDelay(3.0f);
		}

		public void CreateAchievementElement(AchievementElement element, int achivementValue)
		{
			var ui = Instantiate(this.prefabElement, this.achievementParent, false) as ResultAchievementElementController;
			ui.Initialize(element.Title, element.Value(achivementValue));
			this.createdObjects.Add(ui);
		}

		public void CreateAchievementElementFromDateTime(AchievementElement element, AchievementData data)
		{
			var ui = Instantiate(this.prefabElement, this.achievementParent, false) as ResultAchievementElementController;
			ui.Initialize(element.Title, element.ValueFromDateTime(data.PlayTimeHours, data.PlayTimeMinutes, data.PlayTimeSeconds, data.PlayTimeMilliSeconds));
			this.createdObjects.Add(ui);
		}

		private void OnNextFloor()
		{
			this.uiRoot.gameObject.SetActive(false);
			this.createdObjects.ForEach(c => Destroy(c.gameObject));
			this.createdObjects.Clear();
		}

		private void OnPreChangeDungeon()
		{
			this.IsResult = false;
		}
	}
}