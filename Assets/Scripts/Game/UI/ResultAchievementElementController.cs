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
	public class ResultAchievementElementController : MonoBehaviour
	{
		[SerializeField]
		private Text title;

		[SerializeField]
		private Text message;

		public void Initialize(string title, string message)
		{
			this.title.text = title;
			this.message.text = message;
		}
	}
}