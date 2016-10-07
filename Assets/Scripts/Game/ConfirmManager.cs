using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ConfirmManager : SingletonMonoBehaviour<ConfirmManager>
	{
		[SerializeField]
		private GameObject root;

		[SerializeField]
		private Transform content;

		[SerializeField]
		private ConfirmButtonController buttonPrefab;

		public SerializeFieldGetter.StringAssetFinder decideReinforcement;

		public SerializeFieldGetter.StringAssetFinder decideSynthesis;

		public SerializeFieldGetter.StringAssetFinder cancel;

		private List<GameObject> createdObjects = new List<GameObject>();

		public void Add(string message, UnityAction action, bool closeConfirmUI)
		{
			this.root.SetActive(true);
			var button = Instantiate(this.buttonPrefab, this.content, false) as ConfirmButtonController;
			button.Initialize(message, action, closeConfirmUI);
			this.createdObjects.Add(button.gameObject);
		}

		public void Add(SerializeFieldGetter.StringAssetFinder finder, UnityAction action, bool closeConfirmUI)
		{
			this.Add(finder.Element.Get, action, closeConfirmUI);
		}

		public void Add(StringAsset.Finder finder, UnityAction action, bool closeConfirmUI)
		{
			this.Add(finder.Get, action, closeConfirmUI);
		}

		public void Close()
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.createdObjects.Clear();
			this.root.SetActive(false);
		}
	}
}