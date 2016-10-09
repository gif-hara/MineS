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
			onAttack = null,
			onMiss = null, onMissByFear = null,
			onDefeatByPlayer = null,
			onVisibleEnemy = null,
			onLevelUpPlayer = null,
			onContinuousAttack = null,
			onAcquiredItem = null,
			onRecovery = null;

		private Queue<string> messageQueue = new Queue<string>();

		private Coroutine currentMessageCoroutine = null;

		private static string AttackerColor = "attackerColor";

		private static string ReceiverColor = "receiverColor";

		private static string TargetColor = "targetColor";

		public static void OnAttack(IAttack attacker, IAttack receiver, int damage)
		{
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

		public static void OnDefeat(IAttack target)
		{
			var instance = InformationManager.Instance;
			var message = instance.onDefeatByPlayer.Format(target.Name, target.Experience, target.Money).Replace(TargetColor, target.ColorCode);
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