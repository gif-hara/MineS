using UnityEngine;
using System.Collections;

namespace MineS
{
	public class OnDestroyReleaseMode : MonoBehaviour
	{
#if RELEASE
		void Awake()
		{
			Destroy(this.gameObject);
		}
#endif
	}
}
