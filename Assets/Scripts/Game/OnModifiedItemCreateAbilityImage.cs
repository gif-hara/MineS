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
	public class OnModifiedItemCreateAbilityImage : MonoBehaviour, IReceiveModifiedItem
	{
		[SerializeField]
		private Transform root;

		[SerializeField]
		private GameObject prefabBlank;

		[SerializeField]
		private GameObject prefabRegist;

		[SerializeField]
		private GameObject prefabFixRegist;

#region IReceiveModifiedItem implementation

		public void OnModifiedItem(Item item)
		{
			for(int i = 0; i < this.root.childCount; i++)
			{
				Destroy(this.root.GetChild(i).gameObject);
			}

			var equipment = item.InstanceData as EquipmentInstanceData;
			var masterData = (equipment.MasterData as EquipmentMasterData);
			masterData.abilities.ForEach(a => Instantiate(this.prefabFixRegist, this.root, false));
			for(int i = 0, imax = equipment.Abilities.Count - masterData.abilities.Count; i < imax; i++)
			{
				Instantiate(this.prefabRegist, this.root, false);
			}
			for(int i = 0, imax = equipment.BrandingLimit - equipment.Abilities.Count; i < imax; i++)
			{
				Instantiate(this.prefabBlank, this.root, false);
			}
		}

#endregion
	}
}