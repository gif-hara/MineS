using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UniRx;
using UniRx.Triggers;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DamageEffectCreator : MonoBehaviour
	{
		[SerializeField]
		private DamageUI prefab;

		private Queue<System.Action> requests = new Queue<System.Action>();

		private DamageUI currentEffect = null;

		void Start()
		{
			this.LateUpdateAsObservable()
				.Where(_ => this.requests.Count > 0 && this.currentEffect == null)
				.Subscribe(_ =>
			{
				this.requests.Dequeue().Invoke();
			});
		}

		public void ForceRemove()
		{
			for(int i = 0, imax = this.requests.Count - 1; i < imax; i++)
			{
				this.requests.Dequeue();
			}
		}

		public void CreateAsDamage(int damage, Vector3 position, Transform parent)
		{
			this.requests.Enqueue(() =>
			{
				this.currentEffect = this.Create(position, parent);
				this.currentEffect.AsDamage(damage);
			});
		}

		public void CreateAsRecovery(int value, Vector3 position, Transform parent)
		{
			this.requests.Enqueue(() =>
			{
				this.currentEffect = this.Create(position, parent);
				this.currentEffect.AsRecovery(value);
			});
		}

	    public void CreateMiss(Vector3 position, Transform parent)
	    {
	        this.requests.Enqueue(() =>
	        {
	            this.currentEffect = this.Create(position, parent);
	            this.currentEffect.AsMiss();
	        });
	    }

		private DamageUI Create(Vector3 position, Transform parent)
		{
			return Instantiate(this.prefab, position, Quaternion.identity, parent) as DamageUI;
		}
	}
}