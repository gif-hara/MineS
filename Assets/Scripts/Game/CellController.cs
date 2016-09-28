using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;

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
		private Image image;

		[SerializeField][FormerlySerializedAs("text")]
		private Text text;

		public CellData Data{ private set; get; }

		public CharacterDataObserver CharacterDataObserver{ get { return this.characterDataObserver; } }

		public void SetCellData(CellData data)
		{
			this.SetActiveStatusObject(false);
			this.SetText("");
			this.SetImage(null);
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
			if(this.Data == null)
			{
				Debug.LogWarning("CellDataがありません.");
				return;
			}

			this.Data.Action();
		}

		public void DebugAction()
		{
			this.Data.DebugAction();
		}

		public void Description()
		{
			this.Data.Description();
		}

		public void SetText(string message)
		{
			this.text.text = message;
		}

		public void SetImage(Sprite sprite)
		{
			this.image.sprite = sprite;
			this.image.enabled = sprite != null;
		}

		public void SetActiveStatusObject(bool isActive)
		{
			this.SetActive(this.hitPointObject, isActive);
			this.SetActive(this.armorObject, isActive);
			this.SetActive(this.strengthObject, isActive);
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
			this.SetActive(this.notStepObject, !canStep);
		}

		private void ModifiedIdentification(bool isIdentification)
		{
			this.SetActive(this.identificationObject, !isIdentification);
		}

		private void ModifiedLockCount(int lockCount)
		{
			this.SetActive(this.lockObject, lockCount > 0);
		}

		private void SetActive(GameObject gameObject, bool isActive)
		{
			if(gameObject == null)
			{
				return;
			}

			gameObject.SetActive(isActive);
		}
	}
}