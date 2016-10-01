using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateDebugAcquireItemList : MonoBehaviour
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private DebugAcquireItem prefab;

		[SerializeField]
		private List<ItemDataBaseList> itemList;

		void Start()
		{
			this.itemList.ForEach(l => l.Database.ForEach(i =>
			{
				var debugger = Instantiate(this.prefab, this.root, false) as DebugAcquireItem;
				debugger.Initialize(i);
			}));
		}
	}
}