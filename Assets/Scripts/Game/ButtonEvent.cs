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
	public class ButtonEvent : MonoBehaviour
	{
		[SerializeField]
		private CellController cellController;

		private Coroutine deployDescriptionCoroutine = null;

		private const float WaitDeployDescriptionTime = 0.3f;

		public void OnButtonDown()
		{
			this.deployDescriptionCoroutine = StartCoroutine(this.DeployDescription());
		}

		public void OnButtonUp()
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
			this.deployDescriptionCoroutine = null;
			this.cellController.Action();
		}

		public void OnDeselect()
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
			this.deployDescriptionCoroutine = null;
		}

		private IEnumerator DeployDescription()
		{
			yield return new WaitForSecondsRealtime(WaitDeployDescriptionTime);

			this.cellController.Description();
			this.deployDescriptionCoroutine = null;
		}
	}
}