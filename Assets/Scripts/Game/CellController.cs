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
		private CharacterDataObserver characterDataObserver;

		[SerializeField]
		private GameObject hitPointObject;

		[SerializeField]
		private GameObject strengthObject;

		[SerializeField]
		private GameObject armorObject;

		[SerializeField]
		private Text debugText;

		public CellData Data{ private set; get; }

		public CharacterDataObserver CharacterDataObserver{ get { return this.characterDataObserver; } }

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
			this.SetActiveStatusObject(false);
			this.SetDebugText("");
		}

		public void Action()
		{
			this.Data.Action();
		}

		public void DebugAction()
		{
			this.Data.DebugAction();
		}

		public void SetDebugText(string message)
		{
			this.debugText.text = message;
		}

		public void SetActiveStatusObject(bool isActive)
		{
			this.hitPointObject.SetActive(isActive);
			this.armorObject.SetActive(isActive);
			this.strengthObject.SetActive(isActive);
		}

		public void SetStatus(CharacterData data)
		{
			this.SetActiveStatusObject(true);
			this.characterDataObserver.ModifiedData(data);
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