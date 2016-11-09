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
	public abstract class AbilityGrantAbnormalStatusBase : AbilityBase
	{
		public AbilityGrantAbnormalStatusBase(GameDefine.AbilityType abilityType, CharacterData holder, string descriptionKey)
			: base(abilityType, holder, descriptionKey)
		{
			
		}

		public override void OnIdentification(CellData cellData)
		{
			if(this.Holder == null)
			{
				return;
			}

			var visibleEnemies = EnemyManager.Instance.VisibleEnemies;
			visibleEnemies.Remove(this.Holder as EnemyData);
			if(visibleEnemies.Count <= 0)
			{
				return;
			}

			var target = visibleEnemies[Random.Range(0, visibleEnemies.Count)];
			target.AddAbnormalStatus(AbnormalStatusFactory.Create(this.GrantType, target, 10, 0));
		}

		protected abstract GameDefine.AbnormalStatusType GrantType{ get; }
	}
}