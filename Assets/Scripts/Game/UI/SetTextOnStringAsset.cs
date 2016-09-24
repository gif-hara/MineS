using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[ExecuteInEditMode]
	public class SetTextOnStringAsset : MonoBehaviour
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder finder;

		void Start()
		{
#if UNITY_EDITOR
			if(this.target == null)
			{
				this.target = GetComponent<Text>();
			}

			if(!Application.isPlaying)
			{
				return;
			}
#endif
			this.target.text = this.finder.ToString();
		}

#if UNITY_EDITOR
		void Update()
		{
			if(Application.isPlaying)
			{
				return;
			}

			if(this.target == null || this.finder == null || this.finder.guid == "")
			{
				return;
			}

			this.target.text = this.finder.ToString();
		}
#endif
	}
}