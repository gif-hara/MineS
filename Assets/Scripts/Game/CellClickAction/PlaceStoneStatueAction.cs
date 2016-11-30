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
	public class PlaceStoneStatueAction : CellClickActionBase
	{
		[SerializeField]
		private GameDefine.StoneStatueType type;

		private GameObject floatingObject;

		public PlaceStoneStatueAction()
		{
		}

		public PlaceStoneStatueAction(GameDefine.StoneStatueType type)
		{
			this.type = type;
		}

		public override void Invoke(CellData data)
		{
		}

		public override void OnIdentification(CellData cellData)
		{
			base.OnIdentification(cellData);
			this.InternalIdentification(true);
			Object.Instantiate(EffectManager.Instance.prefabInvokeStoneStatue.Element, CanvasManager.Instance.CellField, false);
			var descriptionElement = DescriptionManager.Instance.Data.Get(GameDefine.GetStoneStatueDescriptionKey(this.type));
			InformationManager.InvokeStoneStatue(descriptionElement.Title);
		}

		public override void SetCellData(CellData data)
		{
			Debug.LogFormat("y:{0} x:{1}", data.Position.y, data.Position.x);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.StoneStatue;
			}
		}

		public override Sprite Image
		{
			get
			{
				return TextureManager.Instance.stoneStatueImage.Element;
			}
		}

		public override void OnNextFloor()
		{
			Object.Destroy(this.floatingObject);
		}

		public override void Serialize(int y, int x)
		{
			HK.Framework.SaveData.SetInt(this.GetTypeSerializeKeyName(y, x), (int)this.type);
		}

		public override void Deserialize(int y, int x)
		{
			this.type = (GameDefine.StoneStatueType)HK.Framework.SaveData.GetInt(this.GetTypeSerializeKeyName(y, x));
		}

		public override void LateDeserialize(int y, int x)
		{
			if(!this.cellController.Data.IsIdentification)
			{
				return;
			}

			this.InternalIdentification(false);
		}

		private void InternalIdentification(bool canStartUp)
		{
			this.cellController.SetImage(this.Image);
			var stoneStatue = StoneStatueFactory.Create(this.type);
			if(canStartUp)
			{
				stoneStatue.StartUp();
			}
			CellManager.Instance.AddStoneStatue(stoneStatue);
			this.cellController.Data.BindDeployDescription(new DeployDescriptionOnDescriptionData(GameDefine.GetStoneStatueDescriptionKey(this.type)));
			this.floatingObject = Object.Instantiate(EffectManager.Instance.StoneStatueFloatingObject.Get(this.type), this.cellController.transform, false) as GameObject;
		}

		private string GetTypeSerializeKeyName(int y, int x)
		{
			return string.Format("PlaceStoneStatueActionType_{0}_{1}", y, x);
		}
	}
}