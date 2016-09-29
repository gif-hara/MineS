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
	public class DescriptionManager : SingletonMonoBehaviour<DescriptionManager>
	{
		[SerializeField]
		private List<DescriptionDataObserver> basicObservers;

		[SerializeField]
		private List<CharacterDataObserver> characterDataObservers;

		[SerializeField]
		private DescriptionData data;

		[SerializeField]
		private GameObject emergencyRoot;

		[SerializeField]
		private Text emergencyText;

		public void Deploy(CharacterData data)
		{
			this.characterDataObservers.ForEach(o =>
			{
				o.gameObject.SetActive(true);
				o.ModifiedData(data);
			});
		}

		public void Deploy(string key)
		{
			this.basicObservers.ForEach(o =>
			{
				o.gameObject.SetActive(true);
				o.ModifiedData(this.data.Get(key));
			});
		}

		public void DeployEmergency(string key)
		{
			this.emergencyRoot.SetActive(true);
			this.emergencyText.text = this.data.Get(key).Message;
		}

		public DescriptionData Data{ get { return this.data; } }
	}
}