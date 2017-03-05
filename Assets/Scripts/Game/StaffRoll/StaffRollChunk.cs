using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework;
using System.Collections.Generic;
using System;

namespace MineS
{
	/// <summary>
	/// スタッフロールの一個単位の要素
	/// </summary>
	[CreateAssetMenuAttribute()]
	public class StaffRollChunk : ScriptableObject
	{
        [SerializeField]
        private List<Element> elements;

        public List<Element> Elements
        {
            get 
			{
                return this.elements;
            }
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private StringAsset.Finder message;

            [SerializeField]
            private float visibleDuration;

            public string Message{ get { return this.message.Get; } }

            public float VisibleDuration{ get { return this.visibleDuration; } }
        }
    }
}
