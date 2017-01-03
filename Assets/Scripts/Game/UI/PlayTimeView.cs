using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.UI;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayTimeView : MonoBehaviour
	{
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Text text;

        [SerializeField]
        private StringAsset.Finder format;

        [SerializeField]
        private int defaultCanvasOrder;

        [SerializeField]
        private int alwaysFrontCanvasOrder;

        void Start()
		{
            var optionData = OptionManager.Instance.Data;
            this.OnModifiedVisiblePlayTime(optionData.VisiblePlayTime);
            this.OnModifiedAlwaysFrontPlayTime(optionData.AlwaysFrontPlayTime);
            optionData.AddModifiedVisiblePlayTimeEvent(this.OnModifiedVisiblePlayTime);
            optionData.AddModifiedAlwaysFrontPlayTimeEvent(this.OnModifiedAlwaysFrontPlayTime);
        }

        void LateUpdate()
        {
            if(!OptionManager.Instance.Data.VisiblePlayTime)
            {
                return;
            }
            this.text.text = AchievementManager.Instance.Data.PlayTimeToString(this.format.Get);
        }

		private void OnModifiedVisiblePlayTime(bool value)
		{
            this.text.enabled = value;
        }

		private void OnModifiedAlwaysFrontPlayTime(bool value)
		{
            var order = value ? this.alwaysFrontCanvasOrder : this.defaultCanvasOrder;
            this.canvas.sortingOrder = order;
        }
    }
}