using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework;
using System.Collections.Generic;

namespace MineS
{
	/// <summary>
	/// スタッフロールの一個単位の要素
	/// </summary>
	[CreateAssetMenuAttribute()]
	public class StaffRollChunk : ScriptableObject
	{
        [SerializeField]
        private List<StringAsset.Finder> messages;

        public List<StringAsset.Finder> Messages
        {
            get 
			{
                return this.messages;
            }
        }
    }
}
