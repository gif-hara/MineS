using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedPlayerDataSetGaugeNeedExperience : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Gauge target;

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			var playerData = data as PlayerData;
			Debug.AssertFormat(playerData != null, "PlayerDataにキャストできませんでした.");

			var playerManager = PlayerManager.Instance;
			var experienceData = playerManager.ExperienceData;
			var needNextLevel = experienceData.NeedNextLevel(playerData.Level);
			var currentNeedExperience = needNextLevel - experienceData.Experiences[playerData.Level];
			this.target.Set(1.0f - (needNextLevel - playerData.Experience) / (float)currentNeedExperience);
		}

#endregion
	}
}