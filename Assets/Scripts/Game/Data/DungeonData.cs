using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class DungeonData : DungeonDataBase
	{
		[SerializeField]
		private int floorMax;

		[SerializeField]
		private EnemyTable enemyTable;

		[SerializeField]
		private ItemTable itemTable;

		[SerializeField]
		private TrapTable trapTable;

		[SerializeField]
		private BlackSmithTable blackSmithTable;

		[SerializeField]
		private List<ClearDungeonProccessBase> clearDungeonProccesses;

		[SerializeField]
		private Range createRecoveryItemRange;

		[SerializeField]
		private Range createAnvilRange;

		[SerializeField]
		private Range createMoneyRange;

		[SerializeField]
		private Range createEnemyRange;

		[SerializeField]
		private Range createItemRange;

		[SerializeField]
		private Range createTrapRange;

		[SerializeField]
		private Range acquireMoneyRange;

		public int FloorMax{ get { return this.floorMax; } }

		public Range CreateRecoveryItemRange{ get { return this.createRecoveryItemRange; } }

		public Range CreateAnvilRange{ get { return this.createAnvilRange; } }

		public Range CreateMoneyRange{ get { return this.createMoneyRange; } }

		public Range CreateEnemyRange{ get { return this.createEnemyRange; } }

		public Range CreateItemRange{ get { return this.createItemRange; } }

		public Range CreateTrapRange{ get { return this.createTrapRange; } }

		public Range AcquireMoneyRange{ get { return this.acquireMoneyRange; } }

		[ContextMenu("Check")]
		private void AssertionCheck()
		{
			this.enemyTable.Check(this.floorMax);
		}

		public override CellData[,] Create(CellManager cellManager)
		{
			return new DungeonCreator().Create(cellManager, this, GameDefine.CellRowMax, GameDefine.CellCulumnMax);
		}

		public EnemyData CreateEnemy(int floor, CellController cellController)
		{
			return this.enemyTable.Create(floor, cellController);
		}

		public Item CreateItem()
		{
			return this.itemTable.Create();
		}

		public InvokeTrapActionBase CreateTrap()
		{
			return this.trapTable.Create();
		}

		public bool CanCreateBlackSmith(int floor)
		{
			return this.blackSmithTable.CanCreate(floor);
		}

		public void ClearDungeon()
		{
			this.clearDungeonProccesses.ForEach(c => c.Invoke());
		}

#if UNITY_EDITOR
		[ContextMenu("Apply from Csv")]
		private void Parse()
		{
			var basicData = CsvParser.Parse<List<string>>(
				                AssetDatabase.LoadAssetAtPath("Assets/DataSources/Csv/Dungeon/DungeonBasicData.csv", typeof(TextAsset)) as TextAsset,
				                (s) => s
			                ).First(s => s[0] == this.name);
			this.itemIdentified = bool.Parse(basicData[1]);
			this.floorMax = int.Parse(basicData[2]);
			this.createRecoveryItemRange = new Range(int.Parse(basicData[3]), int.Parse(basicData[4]));
			this.createAnvilRange = new Range(int.Parse(basicData[5]), int.Parse(basicData[6]));
			this.createMoneyRange = new Range(int.Parse(basicData[7]), int.Parse(basicData[8]));
			this.createEnemyRange = new Range(int.Parse(basicData[9]), int.Parse(basicData[10]));
			this.createItemRange = new Range(int.Parse(basicData[11]), int.Parse(basicData[12]));
			this.createTrapRange = new Range(int.Parse(basicData[13]), int.Parse(basicData[14]));
			this.acquireMoneyRange = new Range(int.Parse(basicData[15]), int.Parse(basicData[16]));
			this.mapChipTable = MapChipTable.CreateFromCsv(basicData[0]);
			this.bgmTable = BGMTable.CreateFromCsv(basicData[0]);
			this.shopTable = ShopTable.CreateFromCsv(basicData[0]);
			this.enemyTable = EnemyTable.CreateFromCsv(basicData[0]);
			this.itemTable = ItemTable.CreateFromCsv(basicData[0]);
			this.trapTable = TrapTable.CreateFromCsv(basicData[0]);
			this.blackSmithTable = BlackSmithTable.CreateFromCsv(basicData[0]);
		}
#endif
	}
}