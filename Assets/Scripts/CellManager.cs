using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CellManager : SingletonMonoBehaviour<CellManager>
	{
		[SerializeField]
		private Transform cellField;

		[SerializeField]
		private CellController cellPrefab;

		public List<CellData> Database{ private set; get; }

		private const int CellMax = 8 * 8;

		protected override void Awake()
		{
			base.Awake();
			this.Database = new List<CellData>(CellMax);
		}

		void Start()
		{
			for(int i = 0; i < CellMax; i++)
			{
				(Instantiate(this.cellPrefab, this.cellField, false) as CellController).Initialize(i);
				this.Database.Add(new BlankCell());
			}
		}

		public void SetCell(List<CellData> database)
		{
			Debug.AssertFormat(database.Count == CellMax, "セルの数が合っていません.");
			this.Database = database;
		}
	}
}