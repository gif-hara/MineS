using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Collections;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AutoDestroyOnCompleteAnimation : MonoBehaviour
	{
		[SerializeField]
		private GameObject target;

		[SerializeField]
		private Animator animator;

		IEnumerator Start()
		{
			yield return new WaitWhile(() => this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
			Destroy(this.target);
		}
	}
}