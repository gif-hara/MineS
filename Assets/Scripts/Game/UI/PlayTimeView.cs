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

        void Update()
        {
            this.text.text = AchievementManager.Instance.Data.PlayTimeToString(this.format.Get);
        }
    }
}