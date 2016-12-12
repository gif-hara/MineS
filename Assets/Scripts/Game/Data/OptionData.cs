using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class OptionData
	{
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

		public const float MessageSpeedMax = 2.0f;

		public float BGMVolume{ get { return this.bgmVolume; } }

		public float SEVolume{ get { return this.seVolume; } }

		public float MessageSpeed{ get { return this.messageSpeed; } }

		public bool IsFewMessage{ get { return this.isFewMessage; } }

		public bool AutoSort{ get { return this.autoSort; } }

		public bool SwipeStop{ get { return this.swipeStop; } }

		public OptionData()
		{
			this.bgmVolume = 0.5f;
			this.seVolume = 0.5f;
			this.messageSpeed = 1;
			this.isFewMessage = false;
			this.autoSort = false;
			this.swipeStop = true;
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
	}
}