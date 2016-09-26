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
		private List<DescriptionDataObserver> observers;

		[SerializeField]
		private DescriptionData data;

		[SerializeField]
		private GameObject emergencyRoot;

		[SerializeField]
		private Text emergencyText;

		public void Deploy(CharacterData data)
		{
			for(int i = 0; i < this.observers.Count; i++)
			{
				this.observers[i].ModifiedData(data);
			}
		}

		public void Deploy(string key)
		{
			var data = this.data.Get(key);
			for(int i = 0; i < this.observers.Count; i++)
			{
				this.observers[i].ModifiedData(data);
			}
		}

		public void DeployEmergency(string key)
		{
			this.emergencyRoot.SetActive(true);
			this.emergencyText.text = this.data.Get(key).Message;
		}
	}
}