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

		private CellData data;

		public void Initialize(int id)
		{
			this.id = id;
		}

		public void SetCellData(CellData data)
		{
			this.data = data;
			this.data.BindEvent(this.ModifiedIdentification, this.ModifiedLockCount);
		}

		public void Action()
		{
			this.data.Action();
		}

		public void ModifiedIdentification(bool isIdentification)
		{
			this.identificationObject.SetActive(!isIdentification);
		}

		public void ModifiedLockCount(int lockCount)
		{
			this.lockObject.SetActive(lockCount > 0);
		}
	}
}