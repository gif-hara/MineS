using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class UnityAdsController : SingletonMonoBehaviour<UnityAdsController>
	{
		// public void ShowRewardedAd()
		// {
		// 	this.ShowRewardedAd(this.HandleShowResult);
		// }
		//
		// // public void ShowRewardedAd(System.Action<ShowResult> call)
		// // {
		// // 	if(Advertisement.IsReady("rewardedVideo"))
		// // 	{
		// // 		var options = new ShowOptions { resultCallback = call };
		// // 		Advertisement.Show("rewardedVideo", options);
		// // 	}
		// // }
		//
		// private void HandleShowResult(ShowResult result)
		// {
		// 	switch(result)
		// 	{
		// 	case ShowResult.Finished:
		// 		Debug.Log("The ad was successfully shown.");
		// 			//
		// 			// YOUR CODE TO REWARD THE GAMER
		// 			// Give coins etc.
		// 	break;
		// 	case ShowResult.Skipped:
		// 		Debug.Log("The ad was skipped before reaching the end.");
		// 	break;
		// 	case ShowResult.Failed:
		// 		Debug.LogError("The ad failed to be shown.");
		// 	break;
		// 	}
		// }
	}
}