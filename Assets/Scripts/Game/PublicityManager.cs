using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Advertisements;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PublicityManager : SingletonMonoBehaviour<PublicityManager>
	{
		[SerializeField]
		private Sprite npcImage;

		[SerializeField]
		private StringAsset.Finder watchVideoMessage;

        [SerializeField]
        private StringAsset.Finder chattingMessage;

        [SerializeField]
		private StringAsset.Finder closedMessage;

		[SerializeField]
		private StringAsset.Finder welcomeMessage;

		[SerializeField]
		private StringAsset.Finder goodbyeMessage;

		[SerializeField]
		private TalkChunkData firstTalk;

		[SerializeField]
		private TalkChunkData finishedTalk;

		[SerializeField]
		private TalkChunkData failureTalk;

		[SerializeField]
		private TalkChunkData fullInventoryTalk;

		[SerializeField]
		private List<Rewards> rewards;

        [SerializeField]
        private List<ChatData> chatDatabase;

        [SerializeField]
        private bool debugChatting;

        [SerializeField]
        private TalkChunkData debugChatData;

        private int chattingCount;

        [System.Serializable]
		public class Rewards
		{
			[SerializeField]
			private List<PublicityRewardDataBase> database;

			public bool Grant()
			{
				return this.database[Random.Range(0, this.database.Count)].Grant();
			}
		}

		public void OpenUI()
		{
			this.OpenNPCUI();
			this.CreateConfirm();
			InformationManager.AddMessage(this.welcomeMessage);
		}

		public void OpenNPCUI()
		{
			NPCManager.Instance.Open(this.npcImage);
		}

		public void StartFirstTalk()
		{
			this.OpenNPCUI();
			TalkManager.Instance.StartTalk(this.firstTalk, this.OpenUI);
		}

		public void StartFullInventoryTalk()
		{
			TalkManager.Instance.StartTalk(this.fullInventoryTalk, this.CreateConfirm);
		}

		private void CreateConfirm()
		{
			var confirmManager = ConfirmManager.Instance;
			confirmManager.Add(this.watchVideoMessage, () =>
			{
				if(PlayerManager.Instance.Data.Inventory.IsFreeSpace)
				{
					UnityAdsController.Instance.ShowRewardedAd(this.HandleRewardedAd);
				}
				else
				{
					this.StartFullInventoryTalk();
				}
			}, true);
            confirmManager.Add(this.chattingMessage, () =>
            {
				if(this.debugChatting)
				{
                    TalkManager.Instance.StartTalk(this.debugChatData, this.CreateConfirm);
                }
				else
				{
					var clearDungeonCount = MineS.SaveData.Progress.ClearDungeonCount;
					var talks = this.chatDatabase[clearDungeonCount].Talks;
					TalkManager.Instance.StartTalk(talks[this.chattingCount % talks.Count], this.CreateConfirm);
					++this.chattingCount;
				}
            }, true);
            confirmManager.Add(this.closedMessage, () =>
			{
				InformationManager.AddMessage(this.goodbyeMessage);
				NPCManager.Instance.SetActiveUI(false);
			}, true);
		}

		private void HandleRewardedAd(ShowResult result)
		{
			if(result == ShowResult.Finished)
			{
				TalkManager.Instance.StartTalk(this.finishedTalk, this.GiveReward);
			}
			else
			{
				TalkManager.Instance.StartTalk(this.failureTalk, this.CreateConfirm);
			}
		}

		private void GiveReward()
		{
			if(this.rewards[MineS.SaveData.Progress.ClearDungeonCount].Grant())
			{
				this.CreateConfirm();
			}
		}
	}
}