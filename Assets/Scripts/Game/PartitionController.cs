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
	public class PartitionController : MonoBehaviour
	{
		[SerializeField]
		private Text text;

		public void SetText(string message)
		{
			this.text.text = message;
		}
	}
}