﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class PublicityRewardEquipment : PublicityRewardAddItemBase
	{
		[SerializeField]
		private List<EquipmentData> equipments;

		[EnumLabel("アビリティ", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> randomAddAbilities;

		protected override void InternalGrant()
		{
			var item = new Item(this.equipments[Random.Range(0, this.equipments.Count)]);
			var addAbility = this.randomAddAbilities[Random.Range(0, this.randomAddAbilities.Count)];
			var ability = AbilityFactory.Create(addAbility, null);
			(item.InstanceData as EquipmentInstanceData).AddAbility(ability);
			PlayerManager.Instance.AddItem(item);
		}
	}
}