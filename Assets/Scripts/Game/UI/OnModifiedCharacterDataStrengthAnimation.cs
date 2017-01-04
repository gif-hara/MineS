using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using DG.Tweening;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedCharacterDataStrengthAnimation : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Transform target;

		[SerializeField]
		private Vector3 from;

		[SerializeField]
		private Vector3 to;

		[SerializeField]
		private float duration;

		[SerializeField]
		private Ease ease;

		private int oldStrength = -1;

		private Tweener tweener;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			if(data.Strength == oldStrength)
			{
				return;
			}

			this.oldStrength = data.Strength;
			this.target.localScale = this.from;
			if(this.tweener != null)
			{
				this.tweener.Kill();
			}
			this.tweener = this.target.DOScale(to, this.duration)
				.SetEase(this.ease);
		}

#endregion
	}
}