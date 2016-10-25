using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnModifiedItemCreateAbilityList : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private DescriptionDataObserver observerPrefab;

		private List<GameObject> createdObjects = new List<GameObject>();

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.createdObjects.Clear();

			(item.InstanceData as EquipmentInstanceData).Abilities.ForEach(a =>
			{
				var element = Instantiate(this.observerPrefab, this.root, false) as DescriptionDataObserver;
				element.ModifiedData(a.DescriptionKey);
				this.createdObjects.Add(element.gameObject);
			});
		}

#endregion
	}
}