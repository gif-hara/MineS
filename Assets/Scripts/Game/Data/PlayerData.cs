using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayerData : CharacterData
	{
		public PlayerData()
		{
			this.Initialize("", 100, 100, 4, 0);
		}
	}
}