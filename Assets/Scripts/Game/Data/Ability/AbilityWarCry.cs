using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityWarCry : AbilityBase
	{
		public AbilityWarCry(CharacterData holder)
			: base(GameDefine.AbilityType.Mercy, holder, "WarCry")
		{
		}

		public override void OnIdentification(CellData cellData)
		{
			var cellDatas = EnemyManager.Instance.Enemies.Where(e => !e.Key.IsIdentification && !e.Key.IsLock).Select(e => e.Key).ToList();
			if(cellDatas.Count <= 0)
			{
				return;
			}

			var randomCellData = cellDatas[Random.Range(0, cellDatas.Count)];
			randomCellData.Steppable(false);
			randomCellData.Action();
		}
	}
}