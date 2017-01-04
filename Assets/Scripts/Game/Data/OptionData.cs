using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class OptionData
	{
		public class BoolEvent : UnityEvent<bool>
		{
		}
		[SerializeField]
		private float bgmVolume;

		[SerializeField]
		private float seVolume;

		[SerializeField]
		private float messageSpeed;

		[SerializeField]
		private bool isFewMessage;

		[SerializeField]
		private bool autoSort;

		[SerializeField]
		private bool swipeStop;

        [SerializeField]
        private bool visiblePlayTime;

        [SerializeField]
        private bool alwaysFrontPlayTime;

        private BoolEvent onModifiedVisiblePlayTime = new BoolEvent();

        private BoolEvent onModifiedAlwaysFrontPlayTime = new BoolEvent();

        public const float MessageScrollSpeedMax = 0.5f;

        public const float MessageWaitMax = 1.0f;

		public float BGMVolume{ get { return this.bgmVolume; } }

		public float SEVolume{ get { return this.seVolume; } }

		public float MessageSpeed{ get { return this.messageSpeed; } }

		public bool IsFewMessage{ get { return this.isFewMessage; } }

		public bool AutoSort{ get { return this.autoSort; } }

		public bool SwipeStop{ get { return this.swipeStop; } }

		public bool VisiblePlayTime{ get { return this.visiblePlayTime; } }

		public bool AlwaysFrontPlayTime{get { return this.alwaysFrontPlayTime; } }

        public OptionData()
		{
			this.bgmVolume = 0.5f;
			this.seVolume = 0.5f;
			this.messageSpeed = 1;
			this.isFewMessage = false;
			this.autoSort = false;
			this.swipeStop = true;
            this.visiblePlayTime = false;
        }

		public void SetBGMVolume(float value)
		{
			this.bgmVolume = value;
			HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
		}

		public void SetSEVolume(float value)
		{
			this.seVolume = value;
			HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
		}

		public void SetMessageSpeed(float value)
		{
			this.messageSpeed = value;
			HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
		}

		public void SetIsFewMessage(bool value)
		{
			this.isFewMessage = value;
			HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
		}

		public void SetAutoSort(bool value)
		{
			this.autoSort = value;
			HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
		}

		public void SetSwipeStop(bool value)
		{
			this.swipeStop = value;
			HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
		}

        public void SetVisiblePlayTime(bool value)
        {
            this.visiblePlayTime = value;
            HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
            this.onModifiedVisiblePlayTime.Invoke(value);
        }

		public void SetAlwaysFrontPlayTime(bool value)
		{
            this.alwaysFrontPlayTime = value;
            HK.Framework.SaveData.SetClass<OptionData>(MineS.SaveData.OptionKeyName, this);
            this.onModifiedAlwaysFrontPlayTime.Invoke(value);
        }

		public void AddModifiedVisiblePlayTimeEvent(UnityAction<bool> action)
		{
            this.onModifiedVisiblePlayTime.AddListener(action);
        }

		public void RemoveModifiedVisiblePlayTimeEvent(UnityAction<bool> action)
		{
            this.onModifiedVisiblePlayTime.RemoveListener(action);
        }

		public void AddModifiedAlwaysFrontPlayTimeEvent(UnityAction<bool> action)
		{
            this.onModifiedAlwaysFrontPlayTime.AddListener(action);
        }

		public void RemoveModifiedAlwaysFrontPlayTimeEvent(UnityAction<bool> action)
		{
            this.onModifiedAlwaysFrontPlayTime.RemoveListener(action);
        }
    }
}