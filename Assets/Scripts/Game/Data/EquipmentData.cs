using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class EquipmentData : ItemDataBase
	{
		[SerializeField]
		private int power;

		[SerializeField]
		private GameDefine.ItemType itemType;

		[SerializeField]
		private List<int> spells;

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		public override ItemDataBase Clone
		{
			get
			{
				var result = ScriptableObject.CreateInstance<EquipmentData>();
				this.InternalClone(result);
				result.power = this.power;
				result.itemType = this.itemType;
				result.spells = new List<int>(this.spells);

				return result;
			}
		}
	}
}