using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class TextureManager : SingletonMonoBehaviour<TextureManager>
	{
		[System.Serializable]
		public class AbnormalStatus
		{
			[SerializeField]
			private SerializeFieldGetter.Sprite regeneration;
			[SerializeField]
			private SerializeFieldGetter.Sprite sharpness;
			[SerializeField]
			private SerializeFieldGetter.Sprite curing;
			[SerializeField]
			private SerializeFieldGetter.Sprite xray;
			[SerializeField]
			private SerializeFieldGetter.Sprite trapMaster;
			[SerializeField]
			private SerializeFieldGetter.Sprite happiness;
			[SerializeField]
			private SerializeFieldGetter.Sprite poison;
			[SerializeField]
			private SerializeFieldGetter.Sprite blur;
			[SerializeField]
			private SerializeFieldGetter.Sprite gout;
			[SerializeField]
			private SerializeFieldGetter.Sprite dull;

			private AbnormalStatus()
			{
			}

			public Sprite GetIcon(GameDefine.AbnormalStatusType type)
			{
				switch(type)
				{
				case GameDefine.AbnormalStatusType.Regeneration:
					return this.regeneration.Element;
				case GameDefine.AbnormalStatusType.Sharpness:
					return this.sharpness.Element;
				case GameDefine.AbnormalStatusType.Curing:
					return this.curing.Element;
				case GameDefine.AbnormalStatusType.Xray:
					return this.xray.Element;
				case GameDefine.AbnormalStatusType.TrapMaster:
					return this.trapMaster.Element;
				case GameDefine.AbnormalStatusType.Happiness:
					return this.happiness.Element;
				case GameDefine.AbnormalStatusType.Poison:
					return this.poison.Element;
				case GameDefine.AbnormalStatusType.Blur:
					return this.blur.Element;
				case GameDefine.AbnormalStatusType.Gout:
					return this.gout.Element;
				case GameDefine.AbnormalStatusType.Dull:
					return this.dull.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. {0}", type);
					return null;
				}
			}
		}

		[System.Serializable]
		public class Trap
		{
			public SerializeFieldGetter.Sprite poison;

			public SerializeFieldGetter.Sprite gout;

			public SerializeFieldGetter.Sprite blur;

			public SerializeFieldGetter.Sprite dull;

			public SerializeFieldGetter.Sprite mine;
		}

		[System.Serializable]
		public class Item
		{
			[SerializeField]
			private SerializeFieldGetter.Sprite usableItem;

			[SerializeField]
			private SerializeFieldGetter.Sprite weapon;
			[SerializeField]
			private SerializeFieldGetter.Sprite shield;
			[SerializeField]
			private SerializeFieldGetter.Sprite accessory;
			[SerializeField]
			private SerializeFieldGetter.Sprite helmet;
			[SerializeField]
			private SerializeFieldGetter.Sprite body;
			[SerializeField]
			private SerializeFieldGetter.Sprite glove;
			[SerializeField]
			private SerializeFieldGetter.Sprite leg;

			public Sprite Get(GameDefine.ItemType type)
			{
				switch(type)
				{
				case GameDefine.ItemType.UsableItem:
					return this.usableItem.Element;
				case GameDefine.ItemType.Weapon:
					return this.weapon.Element;
				case GameDefine.ItemType.Shield:
					return this.shield.Element;
				case GameDefine.ItemType.Accessory:
					return this.accessory.Element;
				case GameDefine.ItemType.Helmet:
					return this.helmet.Element;
				case GameDefine.ItemType.Body:
					return this.body.Element;
				case GameDefine.ItemType.Glove:
					return this.glove.Element;
				case GameDefine.ItemType.Leg:
					return this.leg.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. type = {0}", type);
					return null;
				}
			}
		}

		public AbnormalStatus abnormalStatus;

		public Trap trap;

		public Item defaultEquipment;
	}
}