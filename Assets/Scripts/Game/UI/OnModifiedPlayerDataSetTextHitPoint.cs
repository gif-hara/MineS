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
	public class OnModifiedPlayerDataSetTextHitPoint : MonoBehaviour, IReceiveModifiedPlayerData
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

#region IReceiveModifiedPlayerData implementation

		public void OnModifiedPlayerData(PlayerData data)
		{
			this.target.text = this.format.Format(data.HitPoint, data.HitPointMax);
		}

#endregion
	}
}