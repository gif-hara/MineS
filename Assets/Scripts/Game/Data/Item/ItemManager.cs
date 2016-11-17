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
		private ItemMasterDataBaseList magicStoneList;

		[SerializeField]
		private StringAsset usableItemUnidentifiedStringAsset;

		[SerializeField]
		private StringAsset magicStoneUnidentifiedStringAsset;

		public SerializeFieldGetter.StringAssetFinder unidentifiedDescription;

		public SerializeFieldGetter.StringAssetFinder equipmentRevisedLevelName;

		public SerializeFieldGetter.StringAssetFinder unidentifiedItemName;

		public SerializeFieldGetter.StringAssetFinder throwingItemReminaingName;

		public SerializeFieldGetter.StringAssetFinder coatingThrowingItemReminaingName;

		public SerializeFieldGetter.StringAssetFinder magicStoneItemReminaingName;

		public IdentifiedItemManager UsableItemIdentified{ private set; get; }

		public IdentifiedItemManager MagicStoneIdentified{ private set; get; }

		public ItemMasterDataBaseList UsableItemList{ get { return this.usableItemList; } }

		private const string UsableItemSerializeKeyName = "ItemManagerUsableItemIdentified";

		private const string MagicStoneSerializeKeyName = "ItemManagerMagicStoneIdentified";

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
			this.UsableItemIdentified = new IdentifiedItemManager(this.usableItemList, this.usableItemUnidentifiedStringAsset);
			this.MagicStoneIdentified = new IdentifiedItemManager(this.magicStoneList, this.magicStoneUnidentifiedStringAsset);
		}

		public void Serialize()
		{
			this.UsableItemIdentified.Serialize(UsableItemSerializeKeyName);
			this.MagicStoneIdentified.Serialize(MagicStoneSerializeKeyName);
		}

		public void Deserialize()
		{
			this.UsableItemIdentified.Deserialize(UsableItemSerializeKeyName);
			this.MagicStoneIdentified.Deserialize(MagicStoneSerializeKeyName);
		}
	}
}