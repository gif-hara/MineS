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
		private GameObject notStepObject;

		[SerializeField]
		private GameObject identificationObject;

		[SerializeField]
		private GameObject lockObject;

		public CellData Data{ private set; get; }

		public void SetCellData(CellData data)
		{
			this.Data = data;
			this.Data.BindEvent(
				this.Infeasible,
				this.ModifiedCanStep,
				this.ModifiedIdentification,
				this.ModifiedLockCount
			);
		}

		public void Action()
		{
			this.Data.Action();
		}

		public void Infeasible(GameDefine.ActionableType actionableType)
		{
			Debug.Log("Not Executable = " + actionableType);
		}

		public void ModifiedCanStep(bool canStep)
		{
			this.notStepObject.SetActive(!canStep);
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