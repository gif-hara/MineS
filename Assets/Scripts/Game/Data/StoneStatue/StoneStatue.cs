using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class StoneStatue
	{
		protected GameDefine.StoneStatueType type;

		protected CellData cellData;

		public StoneStatue(GameDefine.StoneStatueType type, CellData cellData)
		{
			this.type = type;
			this.cellData = cellData;
		}

		public GameDefine.StoneStatueType Type
		{
			get
			{
				return this.type;
			}
		}

		public virtual void StartUp()
		{
		}

		public virtual void OnLateTurnProgress()
		{
		}
	}
}