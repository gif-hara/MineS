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
	public class UsableItemMasterData : ScriptableObject
	{
		[System.Serializable]
		public class Element : ItemMasterDataBase
		{
			[SerializeField]
			private GameDefine.UsableItemType type;

			[SerializeField]
			private int power0;

			[SerializeField]
			private int power1;

			public GameDefine.UsableItemType UsableItemType{ get { return this.type; } }

			public int Power0{ get { return this.power0; } }

			public int Power1{ get { return this.power1; } }

			public int RandomPower{ get { return Random.Range(this.power0, this.power1 + 1); } }

			public override GameDefine.ItemType ItemType
			{
				get
				{
					return GameDefine.ItemType.UsableItem;
				}
			}
		}

		[SerializeField]
		private List<Element> database;
	}
}