using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using System.Collections;
using System.Text;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InformationManager : SingletonMonoBehaviour<InformationManager>
	{
		[SerializeField]
		private Text text;

		[SerializeField]
		private Transform parent;

		[SerializeField]
		private InformationElement prefabInformation;

		[SerializeField]
		private StringAsset.Finder
			onAttack = null,
			onMiss = null, onMissByFear = null,
			onDefeatByPlayer = null,
			onVisibleEnemy = null,
			onLevelUpPlayer = null,
			onLevelDownPlayer = null,
			onContinuousAttack = null,
			onAcquiredItem = null,
			onRecovery = null,
			onRecoveryArmor = null,
			onInitiativeDamage = null,
			onUseRecoveryHitPointItem = null,
			onUseRecoveryArmorItem = null,
			onUseAddAbnormalStatusItem = null,
			onUseRemoveAbnormalStatusItem = null,
			onUseDamageItem = null,
			onAlsoAddAbnormalStatus = null,
			invalidateAddAbnormalStatus = null,
			hadNoEffect = null,
			onUseNailDown = null,
			onUseCallingOff = null,
			willThrowEnemy = null,
			addBaseStrength = null,
			addHitPointMax = null,
			levelUpEnemy = null,
			onUseAlchemy = null,
			onUseActinidiaByPlayer = null,
			onUseActinidiaByEnemy = null,
			confirmEnterDungeon = null,
			subBaseStrength = null,
			subHitPointMax = null,
			levelDownEnemy = null,
			identifiedItem = null,
			confusionEnemyAttack = null,
			acquireMoney = null,
			gameOver = null,
			gameClear = null,
			closeStair = null,
			returnTownEnemy = null,
			changeCharacter = null;

		private Queue<string> messageQueue = new Queue<string>();

		private Coroutine currentMessageCoroutine = null;

		private const string AttackerColor = "attackerColor";

		private const string ReceiverColor = "receiverColor";

		private const string TargetColor = "targetColor";

		private const string AbnormalStatusColor = "abnormalStatusColor";

		void Start()
		{
			DungeonManager.Instance.AddNextFloorEvent(this.OnNextFloor);
		}

		public static void OnAttack(IAttack attacker, IAttack receiver, int damage)
		{
			if(OptionManager.Instance.Data.IsFewMessage)
			{
				return;
			}
			var instance = InformationManager.Instance;
			var message = instance.onAttack.Format(attacker.Name, receiver.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(ReceiverColor, receiver.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnMiss(IAttack attacker)
		{
			var instance = InformationManager.Instance;
			var message = instance.onMiss.Format(attacker.Name).Replace(AttackerColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnMissByFear(IAttack attacker)
		{
			var instance = InformationManager.Instance;
			var message = instance.onMissByFear.Format(attacker.Name).Replace(AttackerColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnDefeat(IAttack target, int experience)
		{
			var instance = InformationManager.Instance;
			var message = instance.onDefeatByPlayer.Format(target.Name, experience).Replace(TargetColor, target.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnVisibleEnemy(IAttack enemy)
		{
			var instance = InformationManager.Instance;
			var message = instance.onVisibleEnemy.Format(enemy.Name).Replace(TargetColor, enemy.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnLevelUpPlayer(IAttack attacker, int level)
		{
			var instance = InformationManager.Instance;
			var message = instance.onLevelUpPlayer.Format(attacker.Name, level).Replace(TargetColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnLevelDownPlayer(IAttack attacker, int level)
		{
			var instance = InformationManager.Instance;
			var message = instance.onLevelDownPlayer.Format(attacker.Name, level).Replace(TargetColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnContinuousAttack(IAttack attacker, IAttack receiver, int damage)
		{
			var instance = InformationManager.Instance;
			var message = instance.onContinuousAttack.Format(receiver.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(ReceiverColor, receiver.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnAcquiredItem(string itemName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onAcquiredItem.Format(itemName));
		}

		public static void OnRecovery(int value)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onRecovery.Format(value));
		}

		public static void OnRecoveryArmor(int value)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onRecoveryArmor.Format(value));
		}

		public static void OnInitiativeDamage(IAttack attacker, IAttack receiver, int damage)
		{
			var instance = InformationManager.Instance;
			var message = instance.onInitiativeDamage.Format(receiver.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(ReceiverColor, receiver.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseRecoveryHitPointItem(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseRecoveryHitPointItem.Format(user.Name, value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseRecoveryArmorItem(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseRecoveryArmorItem.Format(user.Name, value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseAddAbnormalStatusItem(IAttack user, GameDefine.AbnormalStatusType abnormalStatusType, GameDefine.AddAbnormalStatusResultType addResult)
		{
			var descriptionKey = GameDefine.GetAbnormalStatusDescriptionKey(abnormalStatusType);
			var descriptionData = DescriptionManager.Instance.Data.Get(descriptionKey);
			var instance = InformationManager.Instance;
			var onUseMessage = instance.onUseAddAbnormalStatusItem.Format(user.Name, descriptionData.Title)
				.Replace(TargetColor, user.ColorCode)
				.Replace(AbnormalStatusColor, GameDefine.GetAbnormalStatusColor(abnormalStatusType));
			instance._AddMessage(onUseMessage);

			if(addResult == GameDefine.AddAbnormalStatusResultType.Invalided)
			{
				instance._AddMessage(instance.invalidateAddAbnormalStatus.Get);
			}
		}

		public static void OnUseRemoveAbnormalStatusItem(IAttack user, GameDefine.AbnormalStatusType abnormalStatusType)
		{
			var descriptionKey = GameDefine.GetAbnormalStatusDescriptionKey(abnormalStatusType);
			var descriptionData = DescriptionManager.Instance.Data.Get(descriptionKey);
			var instance = InformationManager.Instance;
			var message = instance.onUseRemoveAbnormalStatusItem.Format(user.Name, descriptionData.Title)
				.Replace(TargetColor, user.ColorCode)
				.Replace(AbnormalStatusColor, GameDefine.GetAbnormalStatusColor(abnormalStatusType));
			instance._AddMessage(message);
		}

		public static void OnUseDamageItem(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseDamageItem.Format(user.Name, value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnAlsoAddAbnormalStatus(GameDefine.AbnormalStatusType abnormalStatusType)
		{
			var descriptionKey = GameDefine.GetAbnormalStatusDescriptionKey(abnormalStatusType);
			var descriptionData = DescriptionManager.Instance.Data.Get(descriptionKey);
			var instance = InformationManager.Instance;
			var message = instance.onAlsoAddAbnormalStatus.Format(descriptionData.Title)
				.Replace(AbnormalStatusColor, GameDefine.GetAbnormalStatusColor(abnormalStatusType));
			instance._AddMessage(message);
		}

		public static void OnHadNoEffect()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.hadNoEffect.Get);
		}

		public static void OnUseNailDown()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onUseNailDown.Get);
		}

		public static void OnUseCallingOff()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onUseCallingOff.Get);
		}

		public static void WillThrowEnemy()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.willThrowEnemy.Get);
		}

		public static void AddBaseStrength(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.addBaseStrength.Format(value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void AddHitPointMax(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.addHitPointMax.Format(value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void LevelUpEnemy(IAttack user, string currentName, string nextName)
		{
			var instance = InformationManager.Instance;
			var message = instance.levelUpEnemy.Format(currentName, nextName)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void SubBaseStrength(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.subBaseStrength.Format(value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void SubHitPointMax(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.subHitPointMax.Format(value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void LevelDownEnemy(IAttack user, string currentName, string nextName)
		{
			var instance = InformationManager.Instance;
			var message = instance.levelDownEnemy.Format(currentName, nextName)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseAlchemy(IAttack user)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseAlchemy.Format(user.Name)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseActinidiaByPlayer(IAttack visibleEnemy)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseActinidiaByPlayer.Format(visibleEnemy.Name)
				.Replace(TargetColor, visibleEnemy.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseActinidiaByEnemy(IAttack user)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseActinidiaByEnemy.Format(user.Name)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void ConfirmEnterDungeon(string dungeonName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.confirmEnterDungeon.Format(dungeonName));
		}

		public static void IdentifiedItem(string unidentifiedItemName, string itemName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.identifiedItem.Format(unidentifiedItemName, itemName));
		}

		public static void ConfusionEnemyAttack(IAttack attacker, IAttack target, int damage)
		{
			if(OptionManager.Instance.Data.IsFewMessage)
			{
				return;
			}

			var instance = InformationManager.Instance;
			var message = instance.confusionEnemyAttack.Format(attacker.Name, target.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(TargetColor, target.ColorCode);
			instance._AddMessage(message);
		}

		public static void AcquireMoney(int money)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.acquireMoney.Format(money));
		}

		public static void GameOver()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.gameOver.Get);
		}

		public static void GameClear(string dungeonName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.gameClear.Format(dungeonName));
		}

		public static void CloseStair()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.closeStair.Get);
		}

		public static void ReturnTown(IAttack user)
		{
			var instance = InformationManager.Instance;
			var message = instance.returnTownEnemy.Format(user.Name)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void ChangeCharacter(string beforeName, IAttack afterCharacter)
		{
			var instance = InformationManager.Instance;
			var message = instance.changeCharacter.Format(beforeName, afterCharacter.Name)
				.Replace(TargetColor, afterCharacter.ColorCode);
			instance._AddMessage(message);
		}

		public static void AddMessage(string message)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(message);
		}

		public static void AddMessage(StringAsset.Finder message)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(message.Get);
		}

		public static void RemoveAllElement()
		{
			if(instance.currentMessageCoroutine != null)
			{
				instance.StopCoroutine(instance.currentMessageCoroutine);
			}
			instance.messageQueue.Clear();
			instance.currentMessageCoroutine = null;
			InformationElement.RemoveAll();
		}

		private void _AddMessage(string message)
		{
			if(this.currentMessageCoroutine == null)
			{
				this.currentMessageCoroutine = StartCoroutine(this.AddMessageCoroutine(message));
			}
			else
			{
				this.messageQueue.Enqueue(message);
			}
		}

		private IEnumerator AddMessageCoroutine(string message)
		{
			var informationElement = Instantiate(this.prefabInformation, this.parent, false) as InformationElement;
			informationElement.Initialize(message);
			yield return new WaitForSeconds(OptionData.MessageSpeedMax - MineS.SaveData.Option.MessageSpeed);

			if(this.messageQueue.Count > 0)
			{
				this.currentMessageCoroutine = StartCoroutine(this.AddMessageCoroutine(this.messageQueue.Dequeue()));
			}
			else
			{
				this.currentMessageCoroutine = null;
			}
		}

		private void OnNextFloor()
		{
			RemoveAllElement();
		}
	}
}