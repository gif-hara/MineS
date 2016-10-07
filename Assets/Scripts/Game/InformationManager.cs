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
		private StringAsset.Finder
			onAttackByPlayer = null, onAttackByEnemy = null,
			onMiss = null, onMissByFear = null,
			onDefeatByPlayer = null,
			onVisibleEnemy = null,
			onLevelUpPlayer = null,
			onContinuousAttack = null,
			onAcquiredItem = null,
			onRecovery = null,
			onNotPossessionEquipment = null,
			confirmReinforceMessage = null,
			notEquipmentLevelUpMessage = null,
			confirmSynthesisSelectTargetEquipment = null,
			notEquipmentSynthesis = null,
			notEquipmentSynthesisTarget = null,
			confirmSynthesisFinalCheck = null;

		private Queue<string> messageQueue = new Queue<string>();

		private Coroutine currentMessageCoroutine = null;

		public static void OnAttackByPlayer(string attackerName, string receiverName, int damage)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onAttackByPlayer.Format(attackerName, receiverName, damage));
		}

		public static void OnAttackByEnemy(string attackerName, string receiverName, int damage)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onAttackByEnemy.Format(attackerName, receiverName, damage));
		}

		public static void OnMiss(string attackerName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onMiss.Format(attackerName));
		}

		public static void OnMissByFear(string attackerName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onMissByFear.Format(attackerName));
		}

		public static void OnDefeatByPlayer(string targetName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onDefeatByPlayer.Format(targetName));
		}

		public static void OnVisibleEnemy(string enemyName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onVisibleEnemy.Format(enemyName));
		}

		public static void OnLevelUpPlayer(int level)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onLevelUpPlayer.Format(level));
		}

		public static void OnContinuousAttack(string targetName, int damage)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onContinuousAttack.Format(targetName, damage));
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

		public static void OnNotPossessionEquipment()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onNotPossessionEquipment.Get);
		}

		public static void OnConfirmReinforcement(int needMoney)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.confirmReinforceMessage.Format(needMoney));
		}

		public static void OnNotEquipmentLevelUp()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.notEquipmentLevelUpMessage.Get);
		}

		public static void OnConfirmBrandingSelectTargetEquipment()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.confirmSynthesisSelectTargetEquipment.Get);
		}

		public static void OnNotEquipmentBranding()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.notEquipmentSynthesis.Get);
		}

		public static void OnConfirmSynthesisFinalCheck(Item baseEquipment, Item targetEquipment, int money)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.confirmSynthesisFinalCheck.Format(baseEquipment.InstanceData.ItemName, targetEquipment.InstanceData.ItemName, money));
		}

		public static void OnNotEquipmentBrandingTarget()
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.notEquipmentSynthesisTarget.Get);
		}

		public static void AddMessage(string message)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(message);
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
			this.text.text += System.Environment.NewLine + message;
			yield return new WaitForSeconds(0.75f);

			if(this.messageQueue.Count > 0)
			{
				this.currentMessageCoroutine = StartCoroutine(this.AddMessageCoroutine(this.messageQueue.Dequeue()));
			}
			else
			{
				this.currentMessageCoroutine = null;
			}
		}
	}
}