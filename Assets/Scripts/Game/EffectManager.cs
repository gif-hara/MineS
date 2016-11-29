using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class EffectManager : SingletonMonoBehaviour<EffectManager>
	{
		[System.Serializable]
		public class _StoneStatueFloatingObject
		{
			[SerializeField]
			private SerializeFieldGetter.GameObject souveir;

			[SerializeField]
			private SerializeFieldGetter.GameObject inbariablyHit;

			[SerializeField]
			private SerializeFieldGetter.GameObject identification;

			[SerializeField]
			private SerializeFieldGetter.GameObject regeneration;

			[SerializeField]
			private SerializeFieldGetter.GameObject poison;

			[SerializeField]
			private SerializeFieldGetter.GameObject light;

			[SerializeField]
			private SerializeFieldGetter.GameObject illusion;

			[SerializeField]
			private SerializeFieldGetter.GameObject springWater;

			[SerializeField]
			private SerializeFieldGetter.GameObject happiness;

			public GameObject Get(GameDefine.StoneStatueType type)
			{
				switch(type)
				{
				case GameDefine.StoneStatueType.Souvenir:
					return this.souveir.Element;
				case GameDefine.StoneStatueType.InbariablyHit:
					return this.inbariablyHit.Element;
				case GameDefine.StoneStatueType.Identification:
					return this.identification.Element;
				case GameDefine.StoneStatueType.Regeneration:
					return this.regeneration.Element;
				case GameDefine.StoneStatueType.Poison:
					return this.poison.Element;
				case GameDefine.StoneStatueType.Light:
					return this.light.Element;
				case GameDefine.StoneStatueType.Illusion:
					return this.illusion.Element;
				case GameDefine.StoneStatueType.SpringWater:
					return this.springWater.Element;
				case GameDefine.StoneStatueType.Happiness:
					return this.happiness.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. type = {0}", type);
					return null;
				}
			}
		}

		public SerializeFieldGetter.GameObject prefabBattleEffectSlash;

		public SerializeFieldGetter.GameObject prefabDeadEffect;

		public SerializeFieldGetter.GameObject prefabStepEffect;

		public SerializeFieldGetter.GameObject prefabPoisonEffect;

		public SerializeFieldGetter.GameObject prefabLevelUp;

		public SerializeFieldGetter.GameObject prefabTakeDamage;

		public SerializeFieldGetter.GameObject prefabSummon;

		public SerializeFieldGetter.GameObject prefabAddAbnormalBuff;

		public SerializeFieldGetter.GameObject prefabAddAbnormalDebuff;

		public SerializeFieldGetter.GameObject prefabDyingEffect;

		public SerializeFieldGetter.GameObject prefabThrowing0;

		public SerializeFieldGetter.GameObject prefabUseMagicStone0;

		public SerializeFieldGetter.GameObject prefabInvokeStoneStatue;

		[SerializeField]
		private ItemObserver prefabSuccessTheft;

		[SerializeField]
		private _StoneStatueFloatingObject stoneStatueFloatingObject;

		public _StoneStatueFloatingObject StoneStatueFloatingObject{ get { return this.stoneStatueFloatingObject; } }

		public void CreateTheftEffect(Item theftedItem, Transform parent)
		{
			(Instantiate(this.prefabSuccessTheft, parent, false) as ItemObserver).ModifiedData(theftedItem);
		}
	}
}