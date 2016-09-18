using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
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

		[SerializeField]
		private Text debugText;

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
			this.Data.SetController(this);
		}

		public void Action()
		{
			this.Data.Action();
		}

		public void SetDebugText(string message)
		{
			this.debugText.text = message;
		}

		private void Infeasible(GameDefine.ActionableType actionableType)
		{
			Debug.Log("Not Executable = " + actionableType);
		}

		private void ModifiedCanStep(bool canStep)
		{
			this.notStepObject.SetActive(!canStep);
		}

		private void ModifiedIdentification(bool isIdentification)
		{
			this.identificationObject.SetActive(!isIdentification);
		}

		private void ModifiedLockCount(int lockCount)
		{
			this.lockObject.SetActive(lockCount > 0);
		}
	}
}