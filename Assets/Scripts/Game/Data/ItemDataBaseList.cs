using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu]
	public class ItemDataBaseList : ScriptableObject
	{
		[SerializeField]
		private List<ItemDataBase> database;

		public List<ItemDataBase> Database{ get { return this.database; } }
	}
}