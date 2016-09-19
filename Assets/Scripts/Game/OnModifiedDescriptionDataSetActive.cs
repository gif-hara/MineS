using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedDescriptionDataSetActive : MonoBehaviour, IReceiveModifiedDescriptionData
	{
		[SerializeField]
		private List<GameObject> targets;

		[SerializeField]
		private bool isActive;

#region IReceiveModifiedDescriptionData implementation

		public void OnModifiedDescriptionData(DescriptionData.Element data)
		{
			this.SetActive();
		}

		public void OnModifiedDescriptionData(CharacterData data)
		{
			this.SetActive();
		}

		private void SetActive()
		{
			for(int i = 0; i < this.targets.Count; i++)
			{
				this.targets[i].SetActive(this.isActive);
			}
		}

#endregion
	}
}