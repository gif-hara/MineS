﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusXray : AbnormalStatusBase
	{
		public AbnormalStatusXray(CharacterData holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
			CellManager.Instance.OnUseXray();
		}

		public override GameDefine.AbnormalStatusType Type
		{
			get
			{
				return GameDefine.AbnormalStatusType.Xray;
			}
		}

		public override GameDefine.AbnormalStatusType OppositeType
		{
			get
			{
				return GameDefine.AbnormalStatusType.None;
			}
		}

		public override GameDefine.AbilityType InvalidateAbilityType
		{
			get
			{
				return GameDefine.AbilityType.None;
			}
		}
	}
}