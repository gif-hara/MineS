using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class RandomMapChipCreator : MapChipCreatorBase
	{
		private int[,] ids;

		public RandomMapChipCreator(int rowMax, int culumnMax)
		{
			this.ids = new int[rowMax, culumnMax];
			this.CreateRoad(GameDefine.AdjacentType.Left, GameDefine.AdjacentType.Right, rowMax, culumnMax);
			this.CreateRoad(GameDefine.AdjacentType.Top, GameDefine.AdjacentType.Bottom, rowMax, culumnMax);
//			if(Random.value > 0.75f)
//			{
//				this.CreateRoad(GameDefine.GetRandomArrow(), GameDefine.GetRandomArrow(), rowMax, culumnMax);
//			}
//			if(Random.value > 0.3f)
//			{
//				this.CreateRoad(GameDefine.GetRandomArrow(), GameDefine.GetRandomArrow(), rowMax, culumnMax);
//			}
		}

		public override int Get(int y, int x)
		{
			return this.ids[y, x];
		}

		private void CreateRoad(GameDefine.AdjacentType startPoint, GameDefine.AdjacentType endPoint, int rowMax, int culumnMax)
		{
			int x = GameDefine.IsVertical(startPoint)
				? Random.Range(1, culumnMax - 1)
				: startPoint == GameDefine.AdjacentType.Left ? 0 : culumnMax - 1;
			int y = GameDefine.IsHorizontal(startPoint)
				? Random.Range(1, rowMax - 1)
				: startPoint == GameDefine.AdjacentType.Top ? 0 : rowMax - 1;
			int endX = GameDefine.IsVertical(endPoint)
				? Random.Range(1, culumnMax - 1)
				: endPoint == GameDefine.AdjacentType.Left ? 0 : culumnMax - 1;
			int endY = GameDefine.IsHorizontal(endPoint)
				? Random.Range(1, rowMax - 1)
				: endPoint == GameDefine.AdjacentType.Top ? 0 : rowMax - 1;
			this.ids[y, x] = 1;
			var direction = GameDefine.GetReverseDirection(startPoint);
			int movementCount = Random.Range(1, GameDefine.IsHorizontal(direction) ? culumnMax - 1 : rowMax - 1);
			for(int i = 0; i < movementCount; i++)
			{
				switch(direction)
				{
				case GameDefine.AdjacentType.Left:
					x--;
				break;
				case GameDefine.AdjacentType.Right:
					x++;
				break;
				case GameDefine.AdjacentType.Top:
					y--;
				break;
				case GameDefine.AdjacentType.Bottom:
					y++;
				break;
				}
				this.ids[y, x] = 1;
			}
			movementCount = GameDefine.IsHorizontal(startPoint) ? Mathf.Abs(endY - y) : Mathf.Abs(endX - x);
			for(int i = 0; i < movementCount; i++)
			{
				if(GameDefine.IsHorizontal(startPoint))
				{
					y += (int)Mathf.Sign(endY - y);
				}
				else
				{
					x += (int)Mathf.Sign(endX - x);
				}
				this.ids[y, x] = 1;
			}
			movementCount = GameDefine.IsHorizontal(startPoint) ? Mathf.Abs(endX - x) : Mathf.Abs(endY - y);
			for(int i = 0; i < movementCount; i++)
			{
				if(GameDefine.IsHorizontal(startPoint))
				{
					x += (int)Mathf.Sign(endX - x);
				}
				else
				{
					y += (int)Mathf.Sign(endY - y);
				}
				this.ids[y, x] = 1;
			}
		}
	}
}