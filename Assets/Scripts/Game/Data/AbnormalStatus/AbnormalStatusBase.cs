using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public abstract class AbnormalStatusBase
	{
		public IAttack Holder{ protected set; get; }

		public int RemainingTurn{ protected set; get; }

		public int WaitTurn{ protected set; get; }

		public abstract GameDefine.AbnormalStatusType Type{ get; }

		public abstract GameDefine.AbnormalStatusType OppositeType{ get; }

		public abstract GameDefine.AbilityType InvalidateAbilityType{ get; }

		public AbnormalStatusBase()
		{
			this.Holder = null;
			this.RemainingTurn = 0;
			this.WaitTurn = 0;
		}

		public AbnormalStatusBase(IAttack holder, int remainingTurn, int waitTurn)
		{
			this.Holder = holder;
			this.RemainingTurn = remainingTurn;
			this.WaitTurn = waitTurn;
		}

		public void SetHolder(IAttack holder)
		{
			this.Holder = holder;
		}

		public virtual void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			if(this.WaitTurn > 0)
			{
				this.WaitTurn--;
				return;
			}

			this.RemainingTurn--;
			this.InternalTurnProgress(type, turnCount);
		}

		public void AddRemainingTurn(int value)
		{
			this.RemainingTurn += value;
			this.RemainingTurn = this.RemainingTurn < 0 ? 0 : this.RemainingTurn;
		}

		public bool IsValid
		{
			get
			{
				return this.RemainingTurn > 0;
			}
		}

		protected virtual void InternalTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
		}

		public void Serialize(string key)
		{
			HK.Framework.SaveData.SetString(GetTypeName(key), this.GetType().FullName);
			HK.Framework.SaveData.SetInt(GetRemainingTurnKeyName(key), this.RemainingTurn);
			HK.Framework.SaveData.SetInt(GetWaitTurnKeyName(key), this.WaitTurn);
		}

		public static AbnormalStatusBase Deserialize(string key)
		{
			if(!HK.Framework.SaveData.ContainsKey(GetTypeName(key)))
			{
				return null;
			}

			var type = System.Type.GetType(HK.Framework.SaveData.GetString(GetTypeName(key)));
			var result = (AbnormalStatusBase)System.Activator.CreateInstance(type);
			result.RemainingTurn = HK.Framework.SaveData.GetInt(GetRemainingTurnKeyName(key));
			result.WaitTurn = HK.Framework.SaveData.GetInt(GetWaitTurnKeyName(key));

			return result;
		}

		private static string GetTypeName(string key)
		{
			return string.Format("{0}_Type", key);
		}

		private static string GetRemainingTurnKeyName(string key)
		{
			return string.Format("{0}_RemainingTurn", key);
		}

		private static string GetWaitTurnKeyName(string key)
		{
			return string.Format("{0}_WaitTurn", key);
		}
	}
}