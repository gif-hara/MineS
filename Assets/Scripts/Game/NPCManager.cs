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
	public class NPCManager : SingletonMonoBehaviour<NPCManager>
	{
		[SerializeField]
		private GameObject uiRoot;

		[SerializeField]
		private Image image;

		public void Open(Sprite image)
		{
			this.SetImage(image);
			this.SetActiveUI(true);
			PlayerManager.Instance.NotifyCharacterDataObservers();
		}

		public void SetImage(Sprite image)
		{
			this.image.sprite = image;
		}

		public void SetActiveUI(bool isActive)
		{
			this.uiRoot.SetActive(isActive);
		}
	}
}