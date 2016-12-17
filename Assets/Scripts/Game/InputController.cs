using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InputController : SingletonMonoBehaviour<InputController>
	{
		[SerializeField]
		private float startSwipeDistance;

		private TouchEvent downAction = new TouchEvent();

		private TouchEvent upAction = new TouchEvent();

		private TouchEvent swipeAction = new TouchEvent();

		public class TouchEvent : UnityEvent<Vector2>
		{
			
		}

		void Update()
		{
			if(HK.Framework.Input.IsTouchDown)
			{
				this.downAction.Invoke(this.TouchPosition);
			}
			if(HK.Framework.Input.IsTouchUp)
			{
				this.upAction.Invoke(this.TouchPosition);
			}
			if(HK.Framework.Input.IsTouch)
			{
				this.swipeAction.Invoke(this.TouchPosition);
			}
		}

		public void AddTouchDownAction(UnityAction<Vector2> action)
		{
			this.downAction.AddListener(action);
		}

		public void RemoveTouchDownAction(UnityAction<Vector2> action)
		{
			this.downAction.RemoveListener(action);
		}

		public void AddTouchUpAction(UnityAction<Vector2> action)
		{
			this.upAction.AddListener(action);
		}

		public void RemoveTouchUpAction(UnityAction<Vector2> action)
		{
			this.upAction.RemoveListener(action);
		}

		public void AddSwipeAction(UnityAction<Vector2> action)
		{
			this.swipeAction.AddListener(action);
		}

		public void RemoveSwipeAction(UnityAction<Vector2> action)
		{
			this.swipeAction.RemoveListener(action);
		}

		private Vector2 TouchPosition
		{
			get
			{
				if(Application.isConsolePlatform || Application.isEditor)
				{
					return UnityEngine.Input.mousePosition;
				}
				else if(Application.isMobilePlatform)
				{
					if(UnityEngine.Input.touches.Length <= 0)
					{
						return Vector2.zero;
					}
					return UnityEngine.Input.touches[0].position;
				}

				Debug.AssertFormat(false, "未対応のプラットフォームです. {0}", Application.platform);
				return Vector2.zero;
			}
		}
	}
}