using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedOptionDataSetButtonDisableInFixDungeon : MonoBehaviour, IReceiveModifiedOptionData
	{
		[SerializeField]
		private Button target;

#region IReceiveModifiedOptionData implementation

		public void OnModifiedOptionData(OptionData data)
		{
			this.target.interactable = DungeonManager.Instance.CurrentDataAsDungeon != null;
		}

#endregion
	}
}