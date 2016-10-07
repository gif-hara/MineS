using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using System.Collections;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CellController : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
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
		private ItemObserver itemObserver;

		[SerializeField]
		private DescriptionDataObserver descriptionDataObserver;

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

		private Coroutine deployDescriptionCoroutine = null;

		private const float WaitDeployDescriptionTime = 0.5f;

#region IPointerDownHandler implementation

		public void OnPointerDown(PointerEventData eventData)
		{
			this.deployDescriptionCoroutine = StartCoroutine(this.DeployDescription());
		}

#endregion

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
			this.deployDescriptionCoroutine = null;
			this.Action();
		}

#endregion

		private IEnumerator DeployDescription()
		{
			yield return new WaitForSecondsRealtime(WaitDeployDescriptionTime);

			this.Description();
			this.deployDescriptionCoroutine = null;
		}


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

			if(PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Confusion) && (!this.Data.IsIdentification || this.Data.CurrentEventType == GameDefine.EventType.Enemy))
			{
				CellManager.Instance.ActionFromConfusion();
			}
			else
			{
				this.Data.Action();
			}
		}

		public void ActionFromConfusion()
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
			if(this.text == null)
			{
				return;
			}
			this.text.text = message;
		}

		public void SetImage(Sprite sprite)
		{
			if(this.image == null)
			{
				return;
			}
			this.image.sprite = sprite;
			this.image.enabled = sprite != null;
		}

		public void SetActiveStatusObject(bool isActive)
		{
			this.SetActive(this.hitPointObject, isActive);
			this.SetActive(this.armorObject, isActive);
			this.SetActive(this.strengthObject, isActive);
		}

		public void SetCharacterData(CharacterData data)
		{
			this.SetActiveStatusObject(true);
			this.characterDataObserver.ModifiedData(data);
		}

		public void SetItemData(Item item)
		{
			this.itemObserver.ModifiedData(item);
		}

		public void SetDescriptionData(DescriptionData.Element data)
		{
			this.descriptionDataObserver.ModifiedData(data);
		}

		public void SetDescriptionData(string key)
		{
			this.descriptionDataObserver.ModifiedData(key);
		}

		public void CancelDeployDescription()
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
			this.deployDescriptionCoroutine = null;
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