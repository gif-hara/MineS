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
        private Text text;

        [SerializeField]
        private StringAsset.Finder format;

		void Start()
		{
            this.text.enabled = OptionManager.Instance.Data.VisiblePlayTime;
            OptionManager.Instance.Data.AddModifiedVisiblePlayTimeEvent(this.OnModifiedVisiblePlayTime);
        }

        void Update()
        {
            this.text.text = AchievementManager.Instance.Data.PlayTimeToString(this.format.Get);
        }

		private void OnModifiedVisiblePlayTime(bool value)
		{
            this.text.enabled = value;
        }
    }
}