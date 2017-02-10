using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UniRx;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class OnTurnProgressSetTextTurn : MonoBehaviour
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private StringAsset.Finder format;

	    [SerializeField]
	    private TurnManager turnManager;

	    void Start()
	    {
	        this.turnManager.Count.SubscribeWithState2(this.target, this.format, (turn, text, _format) => text.text = _format.Format(turn));
	    }
	}
}