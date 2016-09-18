using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public interface IReceiveModifiedPlayerData : IEventSystemHandler
	{
		void OnModifiedPlayerData(PlayerData data);
	}
}