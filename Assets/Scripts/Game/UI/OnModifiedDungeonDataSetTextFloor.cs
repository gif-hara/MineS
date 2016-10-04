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
	public class OnModifiedDungeonDataSetTextFloor : MonoBehaviour, IReceiveModifiedDungeonData
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

#region IReceiveModifiedDungeonData implementation

		public void OnModifiedDungeonData(DungeonData dungeonData)
		{
			this.target.text = this.format.Format(DungeonManager.Instance.Floor);
		}

#endregion
	}
}