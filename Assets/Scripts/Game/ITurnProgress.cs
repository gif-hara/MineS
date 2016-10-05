using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public interface ITurnProgress
	{
		void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount);
	}
}