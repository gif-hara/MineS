using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

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
	}
}