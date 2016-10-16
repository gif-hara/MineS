using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OptionManager : SingletonMonoBehaviour<OptionManager>
	{
		[SerializeField]
		private OptionData data;

		public OptionData Data{ get { return this.data; } }
	}
}