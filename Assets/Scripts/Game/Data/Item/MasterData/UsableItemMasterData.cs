using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class UsableItemMasterData : ItemMasterDataBase
	{
		[SerializeField]
		private GameDefine.UsableItemType type;

		[SerializeField]
		private int power0;

		[SerializeField]
		private int power1;

		[SerializeField]
		private bool canUnidentified;

		[SerializeField]
		private string description;

		[SerializeField]
		private AudioClip useSound;

		public GameDefine.UsableItemType UsableItemType{ get { return this.type; } }

		public int Power0{ get { return this.power0; } }

		public int Power1{ get { return this.power1; } }

		public int RandomPower{ get { return Random.Range(this.power0, this.power1 + 1); } }

		public override bool CanUnidentified{ get { return this.canUnidentified; } }

		public AudioClip UseSound{ get { return this.useSound; } }

		public string Description
		{
			get
			{
				return this.description;
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return GameDefine.ItemType.UsableItem;
			}
		}

		public void OnUse(IAttack user, float rate)
		{
			switch(this.type)
			{
			case GameDefine.UsableItemType.RecoveryHitPointLimit:
				{
					var value = Mathf.FloorToInt(Calculator.GetUsableItemRecoveryValue(this.RandomPower, user) * rate);
					user.RecoveryHitPoint(value, true);
					InformationManager.OnUseRecoveryHitPointItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.RecoveryArmor:
				{
					var value = Mathf.FloorToInt(Calculator.GetUsableItemRecoveryValue(this.RandomPower, user) * rate);
					user.RecoveryArmor(value, true);
					InformationManager.OnUseRecoveryArmorItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.AddAbnormalStatus:
				{
					if(rate > Random.value)
					{
						var type = (GameDefine.AbnormalStatusType)this.Power0;
						var addAbnormalResultType = user.AddAbnormalStatus(AbnormalStatusFactory.Create(type, user, this.Power1, 0));
						InformationManager.OnUseAddAbnormalStatusItem(user, type, addAbnormalResultType);
					}
				}
			break;
			case GameDefine.UsableItemType.RemoveAbnormalStatus:
				{
					if(rate > Random.value)
					{
						var type = (GameDefine.AbnormalStatusType)this.Power0;
						user.RemoveAbnormalStatus(type);
						InformationManager.OnUseRemoveAbnormalStatusItem(user, type);
					}
				}
			break;
			case GameDefine.UsableItemType.Damage:
				{
					var damage = Mathf.FloorToInt(this.RandomPower * rate);
					user.TakeDamageRaw(null, damage, false);
					InformationManager.OnUseDamageItem(user, damage);
					if(user.CharacterType == GameDefine.CharacterType.Enemy && user.IsDead)
					{
						PlayerManager.Instance.Data.Defeat(user);
					}
				}
			break;
			case GameDefine.UsableItemType.NailDown:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						CellManager.Instance.OnUseXrayAll();
						InformationManager.OnUseNailDown();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
				}
			break;
			case GameDefine.UsableItemType.CallingOff:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						CellManager.Instance.RemoveTrap();
						InformationManager.OnUseCallingOff();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
				}
			break;
			case GameDefine.UsableItemType.Drain:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						InformationManager.OnHadNoEffect();
						InformationManager.WillThrowEnemy();
					}
					else
					{
						var playerData = PlayerManager.Instance.Data;
						var damage = Mathf.FloorToInt((user.HitPoint / 2) * rate);
						user.TakeDamageRaw(playerData, damage, true);
						playerData.RecoveryHitPoint(damage, true);
					}
				}
			break;
			case GameDefine.UsableItemType.Brake:
				{
					var damage = Mathf.FloorToInt(user.Armor * rate);
					user.TakeDamageArmorOnly(damage, true);
				}
			break;
			case GameDefine.UsableItemType.UndineDrop:
				{
					var value = Mathf.FloorToInt(this.Power0 * rate);
					user.AddBaseStrength(value);
					InformationManager.AddBaseStrength(user, value);
				}
			break;
			case GameDefine.UsableItemType.UndineTear:
				{
					var value = Mathf.FloorToInt(this.Power0 * rate);
					user.AddHitPointMax(value);
					InformationManager.AddHitPointMax(user, value);
				}
			break;
			case GameDefine.UsableItemType.UndineBlood:
				{
					if(rate > Random.value)
					{
						var value = this.Power0;
						user.ForceLevelUp(value);
					}
				}
			break;
			case GameDefine.UsableItemType.Alchemy:
				{
					if(user.CharacterType == GameDefine.CharacterType.Enemy && rate > Random.value)
					{
						user.ForceDead();
						var cellData = EnemyManager.Instance.InEnemyCells[user as EnemyData];
						var item = DungeonManager.Instance.CreateItem();
						cellData.BindCellClickAction(new AcquireItemAction(item));
						cellData.BindDeployDescription(new DeployDescriptionOnItem(item));
						InformationManager.OnUseAlchemy(user);
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
				}
			break;
			case GameDefine.UsableItemType.Actinidia:
				{
					var blankCell = CellManager.Instance.RandomBlankCell(true);
					if(blankCell == null)
					{
						InformationManager.OnHadNoEffect();
						return;
					}

					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						var enemy = EnemyManager.Instance.Create(blankCell);
						enemy.OnVisible(blankCell);
						InformationManager.OnUseActinidiaByPlayer(enemy);
						PlayerManager.Instance.CloseInventoryUI();
					}
					else if(user.CharacterType == GameDefine.CharacterType.Enemy && rate > Random.value)
					{
						(user as EnemyData).OnDivision(blankCell);
						InformationManager.OnUseActinidiaByEnemy(user);
					}
				}
			break;
			case GameDefine.UsableItemType.TurnBack:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player && DungeonManager.Instance.CanTurnBack(-1))
					{
						DungeonManager.Instance.NextFloorEvent(-1, false);
						PlayerManager.Instance.CloseInventoryUI();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
				}
			break;
			case GameDefine.UsableItemType.Proceed:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						DungeonManager.Instance.NextFloorEvent(1, false);
						PlayerManager.Instance.CloseInventoryUI();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
				}
			break;
			case GameDefine.UsableItemType.OndineDrop:
				{
					var value = Mathf.FloorToInt(this.Power0 * rate);
					user.AddBaseStrength(value);
					InformationManager.SubBaseStrength(user, Mathf.Abs(value));
				}
			break;
			case GameDefine.UsableItemType.OndineTear:
				{
					var value = Mathf.FloorToInt(this.Power0 * rate);
					user.AddHitPointMax(value);
					InformationManager.SubHitPointMax(user, Mathf.Abs(value));
				}
			break;
			case GameDefine.UsableItemType.OndineBlood:
				{
					if(rate > Random.value)
					{
						var value = this.Power0;
						user.ForceLevelDown(value);
					}
				}
			break;
			case GameDefine.UsableItemType.ReturnTown:
				{
					if(rate > Random.value)
					{
						user.ReturnTown();
					}
				}
			break;
			case GameDefine.UsableItemType.Water:
				{
					InformationManager.OnHadNoEffect();
				}
			break;
			default:
				Debug.LogWarning("未実装の使用可能アイテムです UsableItemType= " + this.UsableItemType);
			break;
			}
		}


#if UNITY_EDITOR
		public static UsableItemMasterData CreateFromCsv(List<string> csv)
		{
			var result = CreateInstance<UsableItemMasterData>();
			result.id = int.Parse(csv[0]);
			result.itemName = csv[1];
			result.purchasePrice = int.Parse(csv[2]);
			result.sellingPrice = int.Parse(csv[3]);
			result.image = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/Textures/Item/UsableItem" + csv[4] + ".png", typeof(Sprite)) as Sprite;
			result.useSound = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/DataSources/SE/UseItem" + csv[5] + ".mp3", typeof(AudioClip)) as AudioClip;
			result.type = GameDefine.GetUsableItemType(csv[6]);
			result.power0 = int.Parse(csv[7]);
			result.power1 = int.Parse(csv[8]);
			result.canUnidentified = bool.Parse(csv[9]);
			result.description = csv[10];
			return result;
		}
#endif
	}
}