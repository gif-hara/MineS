//#define BGMDATA_EDITMODE
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using DG.Tweening;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class BGMManager : SingletonMonoBehaviour<BGMManager>
	{
		[SerializeField]
		private AudioSource source;

		[SerializeField]
		private AudioClip debugClip;

		[SerializeField]
		private float startTime;

		[SerializeField]
		private List<BGMData> database;

		private BGMData currentData = null;

#if BGMDATA_EDITMODE
		void Start()
		{
			if(this.debugClip != null)
			{
				this.currentData = this.database.Find(d => d.Clip == this.debugClip);
				this.source.clip = this.debugClip;
				this.source.volume = 1.0f;
				this.source.time = 0.0f;
				this.source.Play();
				this.StartBGM(this.debugClip);
				this.source.time = this.startTime;
			}
		}


#else
		void Start()
		{
			this.SetVolume(MineS.SaveData.Option.BGMVolume);
		}
#endif

		void Update()
		{
			if(!this.source.isPlaying)
			{
				return;
			}

			if(this.source.time >= this.currentData.StartLoop)
			{
				this.source.time = this.currentData.EndLoop;
#if BGMDATA_EDITMODE
				Debug.Log("Loop");
#endif
			}

#if BGMDATA_EDITMODE
			var addValue = Input.GetKey(KeyCode.LeftCommand) ? 0.01f : 0.001f;
			if(!Input.GetKey(KeyCode.LeftShift))
			{
				if(Input.GetKeyDown(KeyCode.LeftArrow))
				{
					this.currentData.startLoop -= addValue;
					Debug.LogFormat("startLoop = {0}", this.currentData.startLoop);
					this.SetStartTime();
				}
				if(Input.GetKeyDown(KeyCode.RightArrow))
				{
					this.currentData.startLoop += addValue;
					Debug.LogFormat("startLoop = {0}", this.currentData.startLoop);
					this.SetStartTime();
				}
			}
			else
			{
				if(Input.GetKeyDown(KeyCode.LeftArrow))
				{
					this.currentData.endLoop -= addValue;
					Debug.LogFormat("endLoop = {0}", this.currentData.endLoop);
					this.SetStartTime();
				}
				if(Input.GetKeyDown(KeyCode.RightArrow))
				{
					this.currentData.endLoop += addValue;
					Debug.LogFormat("endLoop = {0}", this.currentData.endLoop);
					this.SetStartTime();
				}
			}

			if(Input.GetKeyDown(KeyCode.Space))
			{
				Debug.LogFormat("time = {0}", this.source.time);
			}
#endif
		}

		public void FadeOut()
		{
			this.source.DOFade(0.0f, 1.0f);
		}

		public void StartBGM(AudioClip clip)
		{
#if !BGMDATA_EDITMODE
			this.currentData = this.database.Find(d => d.Clip == clip);
			this.source.clip = clip;
			this.source.time = 0.0f;
			this.source.Play();
#endif
		}

		public void SetVolume(float value)
		{
			this.source.volume = value;
		}

		[ContextMenu("Set StartTime")]
		private void SetStartTime()
		{
			this.source.time = this.startTime;
		}
	}
}