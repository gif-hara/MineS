using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DamageUI : MonoBehaviour
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private Color recoveryColor;

		[SerializeField]
		private Color damageColor;

		public void AsDamage(int damage)
		{
			this.Set(damage, this.damageColor);
		}

		public void AsRecovery(int damage)
		{
			this.Set(damage, this.recoveryColor);
		}

		private void Set(int damage, Color color)
		{
			this.target.text = damage.ToString();
			this.target.color = color;
		}
	}
}