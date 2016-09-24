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
	public class IconManager : SingletonMonoBehaviour<IconManager>
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
			private SerializeFieldGetter.Sprite spirit;
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
			private SerializeFieldGetter.Sprite headache;
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
				case GameDefine.AbnormalStatusType.Spirit:
					return this.spirit.Element;
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
				case GameDefine.AbnormalStatusType.Headache:
					return this.headache.Element;
				case GameDefine.AbnormalStatusType.Dull:
					return this.dull.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. {0}", type);
					return null;
				}
			}
		}

		public AbnormalStatus abnormalStatus;
	}
}