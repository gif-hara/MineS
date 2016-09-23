using UnityEngine;
using System.Collections;

namespace MineS
{
    public class OnDestroyReleaseMode : MonoBehaviour
    {
#if Release
        void Awake()
        {
            Destroy(this.gameObject);
        }
#endif
    }
}
