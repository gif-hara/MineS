using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[ExecuteInEditMode()]
	public class OptionManager : SingletonMonoBehaviour<OptionManager>
	{
		[SerializeField]
		private GameObject ui;

		[SerializeField]
		private List<GameObject> observers;

		[SerializeField]
		private OptionData data;

		public OptionData Data{ get { return this.data; } }

		protected override void Awake()
		{
			base.Awake();
			this.data = MineS.SaveData.Option;
		}

		void OnValidate()
		{
		}

		public void OpenUI()
		{
			this.ui.SetActive(true);
			ExecuteEventsExtensions.Execute<IReceiveModifiedOptionData>(this.observers, null, (handler, eventData) => handler.OnModifiedOptionData(this.data));
		}

		public void CloseUI()
		{
			this.ui.SetActive(false);
		}

		public void SetBGMVolume(float value)
		{
			this.Data.SetBGMVolume(value);
		}

		public void SetSEVolume(float value)
		{
			this.Data.SetSEVolume(value);
		}

		public void SetMessageSpeed(float value)
		{
			this.Data.SetMessageSpeed(value);
		}

		public void SetIsFewMessage(bool value)
		{
			this.Data.SetIsFewMessage(value);
		}

		public void SetAutoSort(bool value)
		{
			this.Data.SetAutoSort(value);
		}

	}
}