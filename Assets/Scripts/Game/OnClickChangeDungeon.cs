using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnClickChangeDungeon : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private DungeonDataBase dungeonData;

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			DungeonManager.Instance.ChangeDungeonData(this.dungeonData);
		}

#endregion
	}
}