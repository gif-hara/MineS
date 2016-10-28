using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public abstract class TalkDataBase : ScriptableObject
	{
		[SerializeField]
		private StringAsset.Finder talkElement;

		public abstract void Invoke();

		protected void Talk()
		{
			InformationManager.AddMessage(this.talkElement.Get);
		}
	}
}