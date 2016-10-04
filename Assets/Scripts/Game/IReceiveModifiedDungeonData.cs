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
	public interface IReceiveModifiedDungeonData : IEventSystemHandler
	{
		void OnModifiedDungeonData(DungeonData dungeonData);
	}
}