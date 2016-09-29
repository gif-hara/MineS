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
	[System.Serializable]
	public abstract class ItemDataBase : ScriptableObject
	{
		[SerializeField]
		protected StringAsset.Finder itemName;

		[SerializeField]
		private StringAsset.Finder description;

		[SerializeField]
		protected GameDefine.RareType rare;

		[SerializeField]
		private Sprite image;

		public string ItemName{ get { return this.itemName.ToString(); } }

		public string Description{ get { return this.description.ToString(); } }

		public GameDefine.RareType RareType{ get { return this.rare; } }

		public Sprite Image{ get { return this.image; } }

		public abstract GameDefine.ItemType ItemType{ get; }

		public abstract ItemDataBase Clone{ get; }

		protected void InternalClone(ItemDataBase clone)
		{
			clone.itemName = this.itemName;
			clone.rare = this.rare;
			clone.image = this.image;
		}
	}
}