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

		private List<CellController> cells = new List<CellController>();

		private const int CellMax = 8 * 8;

		protected override void Awake()
		{
			base.Awake();
		}

		void Start()
		{
			var database = new List<CellData>();
			for(int i = 0; i < CellMax; i++)
			{
				var cell = (Instantiate(this.cellPrefab, this.cellField, false) as CellController);
				cell.Initialize(i);
				this.cells.Add(cell);
				database.Add(new BlankCell());
			}
			this.SetCell(database);
		}

		public void SetCell(List<CellData> database)
		{
			Debug.AssertFormat(this.cells.Count == database.Count, "セルの数が合っていません.");
			for(int i = 0, imax = this.cells.Count; i < imax; i++)
			{
				this.cells[i].SetCellData(database[i]);
			}
		}
	}
}