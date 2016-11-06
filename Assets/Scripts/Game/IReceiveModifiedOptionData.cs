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
	public interface IReceiveModifiedOptionData : IEventSystemHandler
	{
		void OnModifiedOptionData(OptionData data);
	}
}