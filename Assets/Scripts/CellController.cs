using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CellController : MonoBehaviour
	{
		[SerializeField]
		private GameObject identificationObject;

		[SerializeField]
		private GameObject lockObject;

		private int id;

		public void Initialize(int id)
		{
			this.id = id;
		}

		public void Action()
		{
			CellManager.Instance.Database[this.id].Action();
		}
	}
}