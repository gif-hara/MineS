using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityBase : ITurnProgress, IIdentification
	{
		public GameDefine.AbilityType Type{ protected set; get; }

		public CharacterData Holder{ protected set; get; }

		public string DescriptionKey{ protected set; get; }

		public AbilityBase(GameDefine.AbilityType type, CharacterData holder, string descriptionKey)
		{
			this.Type = type;
			this.SetHolder(holder);
			this.DescriptionKey = descriptionKey;
		}

		public virtual void SetHolder(CharacterData holder)
		{
			this.Holder = holder;
		}

		public string Name
		{
			get
			{
				return DescriptionManager.Instance.Data.Get(this.DescriptionKey).Title;
			}
		}

		public virtual void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			
		}

		public virtual void OnIdentification(CellData cellData)
		{
			
		}
	}
}