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

	    [SerializeField]
	    private StringAsset.Finder missMessage;

		public void AsDamage(int damage)
		{
			this.Set(damage.ToString(), this.damageColor);
		}

		public void AsRecovery(int damage)
		{
			this.Set(damage.ToString(), this.recoveryColor);
		}

	    public void AsMiss()
	    {
	        this.Set(this.missMessage.Get, this.damageColor);
	    }

	    private void Set(string message, Color color)
		{
			this.target.text = message;
			this.target.color = color;
		}
	}
}