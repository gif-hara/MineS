using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InformationElement : MonoBehaviour
	{
		[SerializeField]
		private Text text;

		[SerializeField]
		private ContentSizeFitter contentSizeFitter;

		private RectTransform rectTransform;

		private static List<InformationElement> elements = new List<InformationElement>();

		void Awake()
		{
			elements.Add(this);
		}

		public void Initialize(string message)
		{
			this.rectTransform = this.transform as RectTransform;
			this.UpdateAsObservable().First().Subscribe(_ =>
			{
				this.text.text = message;
				this.contentSizeFitter.SetLayoutVertical();
				this.rectTransform.anchoredPosition = new Vector2(0.0f, -this.rectTransform.sizeDelta.y);
				this.Move(this.rectTransform.sizeDelta.y);
			});
		}

		public void Move(float positionY)
		{
			elements.RemoveAll(e => e == null);
			elements.ForEach(e =>
			{
				DOTween.To(
					() => e.rectTransform.anchoredPosition.y,
					y => e.rectTransform.anchoredPosition = new Vector2(e.rectTransform.anchoredPosition.x, y),
					e.rectTransform.anchoredPosition.y + positionY,
					(OptionData.MessageSpeedMax - MineS.SaveData.Option.MessageSpeed) / 2)
					.SetEase(Ease.OutFlash)
					.OnComplete(() =>
				{
					if(e.rectTransform.anchoredPosition.y > e.rectTransform.sizeDelta.y)
					{
						Destroy(e.gameObject);
					}
				});
			});
		}

		public static void RemoveAll()
		{
			elements.RemoveAll(e => e == null);
			elements.ForEach(e => Destroy(e.gameObject));
		}
	}
}