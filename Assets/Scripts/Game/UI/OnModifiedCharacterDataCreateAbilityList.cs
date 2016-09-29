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
	public class OnModifiedCharacterDataCreateAbilityList : MonoBehaviour, IReceiveModifiedCharacterData
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private DescriptionDataObserver observerPrefab;

		private List<GameObject> createdObjects = new List<GameObject>();

#region IReceiveModifiedCharacterData implementation

		public void OnModifiedCharacterData(CharacterData data)
		{
			this.createdObjects.ForEach(o => Destroy(o));
			this.createdObjects.Clear();

			data.Abilities.ForEach(a =>
			{
				var element = Instantiate(this.observerPrefab, this.root, false) as DescriptionDataObserver;
				element.ModifiedData(a.DescriptionKey);
				this.createdObjects.Add(element.gameObject);
			});
		}

#endregion
	}
}