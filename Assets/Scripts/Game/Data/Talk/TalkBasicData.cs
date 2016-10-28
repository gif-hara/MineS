using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class TalkBasicData : TalkDataBase
	{
		[SerializeField]
		private StringAsset.Finder nextMessage;

		public override void Invoke()
		{
			this.Talk();

			ConfirmManager.Instance.Add(this.nextMessage, () =>
			{
				TalkManager.Instance.Next();
			}, true);
		}
	}
}