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
	public interface IReceiveModifiedDescriptionData : IEventSystemHandler
	{
		void OnModifiedDescriptionData(DescriptionData.Element data);

		void OnModifiedDescriptionData(CharacterData data);
	}
}