using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

namespace MineS
{
	/// <summary>
	/// スタッフロールを制御するヤーツ
	/// </summary>
	public class StaffRollManager : SingletonMonoBehaviour<StaffRollManager>
	{
        [SerializeField]
        private StaffRollElement prefab;

        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private float createDelay;

        [SerializeField]
        private float visibleDelay;

        [SerializeField]
        private float completeFadeOutDelay;

        [SerializeField]
        private float completeFadeOutDuration;

        [SerializeField]
        private List<StaffRollChunk> chunks;

        private List<StaffRollElement> currentElements = new List<StaffRollElement>();

        private List<StaffRollElement> removeElements = new List<StaffRollElement>();

        private int visibleChunkCount;

		void Start()
		{
            this.StartStaffRoll();
        }

        public void StartStaffRoll()
		{
            this.visibleChunkCount = 0;
            this.canvas.enabled = true;
            this.canvasGroup.alpha = 1.0f;
            StartCoroutine(this.CreateElement(this.chunks[this.visibleChunkCount]));
        }

        public void Complete(StaffRollElement element)
        {
            this.removeElements.Add(element);
            this.currentElements.Remove(element);
			if(this.currentElements.Count > 0)
			{
                return;
            }

            this.removeElements.ForEach(e => Destroy(e.gameObject));
            this.removeElements.Clear();

            ++this.visibleChunkCount;
			if(this.chunks.Count > this.visibleChunkCount)
			{
                StartCoroutine(this.CreateElement(this.chunks[this.visibleChunkCount]));
            }
			else
			{
                this.StartFadeOut();
            }
        }

		private void StartFadeOut()
		{
            DOTween.To(() => this.canvasGroup.alpha, a => this.canvasGroup.alpha = a, 0.0f, this.completeFadeOutDuration)
            .SetDelay(this.completeFadeOutDelay)
            .OnComplete(() =>
            {
                this.canvas.enabled = false;
            });
        }

        private IEnumerator CreateElement(StaffRollChunk chunk)
        {
            yield return new WaitForSeconds(this.createDelay);

            for (var i = 0; i < chunk.Messages.Count; i++)
            {
                var element = Instantiate(this.prefab, this.transform, false);
                element.Setup(this, chunk.Messages[i].Get, this.visibleDelay * i);
                this.currentElements.Add(element);
            }
        }
    }
}
