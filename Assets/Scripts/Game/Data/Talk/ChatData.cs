using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineS
{
	[Serializable]
	public class ChatData
	{
        [SerializeField]
        private List<TalkChunkData> talks;

		public List<TalkChunkData> Talks{ get { return this.talks; } }
    }
}
