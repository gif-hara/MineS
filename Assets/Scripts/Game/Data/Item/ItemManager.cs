using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class ItemManager : SingletonMonoBehaviour<ItemManager>
	{
		[SerializeField]
		private ItemMasterDataBaseList usableItemList;

		[SerializeField]
		private StringAsset unidentifiedStringAsset;

		public SerializeFieldGetter.StringAssetFinder unidentifiedDescription;

		public SerializeFieldGetter.StringAssetFinder equipmentRevisedLevelName;

		public SerializeFieldGetter.StringAssetFinder unidentifiedItemName;

		public SerializeFieldGetter.StringAssetFinder throwingItemReminaingName;

		public SerializeFieldGetter.StringAssetFinder coatingThrowingItemReminaingName;

		public SerializeFieldGetter.StringAssetFinder magicStoneItemReminaingName;

		public IdentifiedItemManager UsableItemIdentified{ private set; get; }

		private const string UsableItemSerializeKeyName = "ItemManagerUsableItemIdentified";

		public ItemMasterDataBaseList UsableItemList{ get { return this.usableItemList; } }

		protected override void Awake()
		{
			base.Awake();
			this.Initialize();
		}

		void Start()
		{
			DungeonManager.Instance.AddChangeDungeonEvent(this.Initialize);
		}

		public void Initialize()
		{
			this.UsableItemIdentified = new IdentifiedItemManager(this.usableItemList, unidentifiedStringAsset);
		}

		public void Serialize()
		{
			this.UsableItemIdentified.Serialize(UsableItemSerializeKeyName);
		}

		public void Deserialize()
		{
			this.UsableItemIdentified.Deserialize(UsableItemSerializeKeyName);
		}
	}
}